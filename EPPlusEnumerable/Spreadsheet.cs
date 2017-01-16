using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace EPPlusEnumerable
{
    public static class Spreadsheet
    {
        #region Static Fields

        private static readonly char[] Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();

        private const TableStyles DefaultTableStyle = TableStyles.Medium16;

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates an Excel spreadsheet with worksheets for each collection of objects.
        /// </summary>
        /// <param name="data">A collection of data collections. Each outer collection will be used as a worksheet, while the inner collections will be used as data rows.</param>
        /// <returns>An Excel spreadsheet as a byte array.</returns>
        public static byte[] Create(IEnumerable<IEnumerable<object>> data) => CreatePackage(data).GetAsByteArray();

        /// <summary>
        /// Creates an Excel spreadsheet with worksheets for each collection of objects.
        /// </summary>
        /// <param name="data">A collection of data collections. Each outer collection will be used as a worksheet, while the inner collections will be used as data rows.</param>
        /// <returns>A populated ExcelPackage object.</returns>
        public static ExcelPackage CreatePackage(IEnumerable<IEnumerable<object>> data)
        {
            var package = new ExcelPackage();

            var collections = data as IList<IEnumerable<object>> ?? data.ToList();
            foreach (var collection in collections)
            {
                AddWorksheet(package, collection as IList<object> ?? collection.ToList());
            }

            AddSpreadsheetLinks(package, collections);

            return package;
        }

        /// <summary>
        /// Creates an Excel spreadsheet with a single worksheet for the supplied data.
        /// </summary>
        /// <param name="data">Each row of the spreadsheet will contain one item from the data collection.</param>
        /// <returns>An Excel spreadsheet as a byte array.</returns>
        public static byte[] Create(IEnumerable<object> data) => CreatePackage(data).GetAsByteArray();

        /// <summary>
        /// Creates an Excel spreadsheet with a single worksheet for the supplied data.
        /// </summary>
        /// <param name="data">Each row of the spreadsheet will contain one item from the data collection.</param>
        /// <returns>A populated ExcelPackage object.</returns>
        public static ExcelPackage CreatePackage(IEnumerable<object> data)
        {
            var package = new ExcelPackage();

            var list = data as IList<object> ?? data.ToList();
            AddWorksheet(package, list);
            AddSpreadsheetLinks(package, new[] { list });

            return package;
        }

        /// <summary>
        /// Create a single worksheet with the supplied data.
        /// </summary>
        /// <param name="package">The existing package</param>
        /// <param name="worksheetName">The name of the new worksheet</param>
        /// <param name="data">Each row of the spreadsheet will contain one item from the data collection.</param>
        /// <returns></returns>
        public static ExcelWorksheet CreateWorksheet(ExcelPackage package, string worksheetName, IEnumerable<object> data)
        {
            var list = data as IList<object> ?? data.ToList();
            var retVal = AddWorksheet(package, list, worksheetName);
            AddSpreadsheetLinks(package, new[] { list });

            return retVal;
        }
        #endregion

        #region Private Methods

        private static ExcelWorksheet AddWorksheet(ExcelPackage package, ICollection<object> data, string worksheetName = null)
        {
            if (data == null || !data.Any())
                return null;

            var firstRow = data.First();
            var collectionType = firstRow.GetType();
            var properties = collectionType.GetProperties();
            worksheetName = worksheetName ?? GetWorksheetName(firstRow, collectionType);
            var worksheet = package.Workbook.Worksheets.Add(worksheetName);
            var col = 0;

            // add column headings
            foreach (var property in properties)
            {
                var propertyName = GetPropertyName(property);

                if (property.GetCustomAttribute<SpreadsheetExcludeAttribute>() != null)
                {
                    // this property has a SpreadsheetExcludeAttribute
                    continue;
                }

                col += 1;
                worksheet.Cells[$"{GetColumnLetter(col)}1"].Value = propertyName;
            }

            // add rows (starting with two, since Excel is 1-based and we added a row of column headings)
            for (var row = 2; row < data.Count + 2; row++)
            {
                var item = data.ElementAt(row - 2);
                col = 0;

                for (var i = 0; i < properties.Length; i++)
                {
                    var property = properties.ElementAt(i);

                    if (property.GetCustomAttribute<SpreadsheetExcludeAttribute>() != null)
                    {
                        continue;
                    }

                    col += 1;
                    var cell = $"{GetColumnLetter(col)}{row}";
                    //var value = property.GetValue(item) ?? string.Empty;
                    worksheet.Cells[cell].Value = GetPropertyValue(property, item);
                }
            }

            // set table formatting
            using (var range = worksheet.Cells[$"A1:{GetColumnLetter(col)}{data.Count + 1}"])
            {
                range.AutoFitColumns();

                var table = worksheet.Tables.Add(range, "table_" + worksheetName.Replace(" ", string.Empty));
                table.TableStyle = GetTableStyle(collectionType);
            }

            return worksheet;
        }

        private static string GetWorksheetName(object firstRow, Type collectionType)
        {
            var worksheetName = collectionType.Name;

            // this is just to strip out the giant string of numbers that EntityFramework appends to
            // proxy types if you passed in a collection of those rather than a type with a
            // displayname attribute specified
            if (worksheetName.Contains('_'))
            {
                worksheetName = worksheetName.Substring(0, worksheetName.IndexOf('_'));
            }

            var worksheetNameProperty = collectionType
                .GetProperties()
                .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(SpreadsheetTabNameAttribute), true));

            if (worksheetNameProperty != null)
            {
                var worksheetNameAttribute = worksheetNameProperty.GetCustomAttribute<SpreadsheetTabNameAttribute>(true);
                var worksheetPropertyValue = worksheetNameProperty.GetValue(firstRow);

                worksheetName = !string.IsNullOrWhiteSpace(worksheetNameAttribute.FormatString) ? string.Format(worksheetNameAttribute.FormatString, worksheetPropertyValue) : worksheetPropertyValue.ToString();
            }
            else
            {
                var displayNameAttribute = collectionType.GetCustomAttribute<DisplayNameAttribute>(true);
                if (displayNameAttribute != null)
                {
                    worksheetName = displayNameAttribute.DisplayName;
                }
                else
                {
                    var displayAttribute = collectionType.GetCustomAttribute<DisplayAttribute>(true);
                    if (displayAttribute != null)
                    {
                        worksheetName = displayAttribute.Name;
                    }
                }
            }

            return worksheetName;
        }

        private static TableStyles GetTableStyle(Type collectionType)
        {
            var tableStyle = DefaultTableStyle;

            var spreadsheetTableStyleAttribute = collectionType.GetCustomAttribute<SpreadsheetTableStyleAttribute>(true);
            if (spreadsheetTableStyleAttribute != null)
            {
                tableStyle = spreadsheetTableStyleAttribute.TableStyle;
            }

            return tableStyle;
        }

        private static string GetPropertyName(MemberInfo property)
        {
            var propertyName = property.Name;

            var displayNameAttribute = property.GetCustomAttribute<DisplayNameAttribute>(true);
            if (displayNameAttribute != null)
            {
                propertyName = displayNameAttribute.DisplayName;
            }
            else
            {
                var displayAttribute = property.GetCustomAttribute<DisplayAttribute>(true);
                if (displayAttribute != null)
                {
                    propertyName = displayAttribute.Name;
                }
            }

            return propertyName;
        }

        private static object GetPropertyValue(PropertyInfo property, object item)
        {
            var inputValue = property.GetValue(item);
            object outputValue = string.Empty;

            var displayFormatAttribute = property.GetCustomAttribute<DisplayFormatAttribute>(true);
            if (displayFormatAttribute != null)
            {
                if (inputValue == null && !string.IsNullOrWhiteSpace(displayFormatAttribute.NullDisplayText))
                {
                    outputValue = displayFormatAttribute.NullDisplayText;
                }
                else if (inputValue != null)
                {
                    outputValue = string.Format(displayFormatAttribute.DataFormatString, inputValue);
                }
            }
            else if (inputValue != null)
            {
                if (property.PropertyType.IsValueType)
                {
                    // for value types, just output the raw value
                    outputValue = inputValue;
                }
                else
                {
                    var enumerable = (inputValue as IEnumerable<object>);
                    if (enumerable != null)
                    {
                        // for collections, return a count
                        outputValue = enumerable.Count();
                    }
                    else
                    {
                        // reference type
                        outputValue = inputValue.ToString();
                    }
                }
            }

            return outputValue;
        }

        private static void AddSpreadsheetLinks(ExcelPackage package, IEnumerable<IEnumerable<object>> data)
        {
            foreach (var collection in data)
            {
                var objects = collection as IList<object> ?? collection.ToList();
                if (collection == null || !objects.Any())
                {
                    continue;
                }

                var firstRow = objects.First();
                var collectionType = firstRow.GetType();
                var properties = collectionType.GetProperties();
                var worksheetName = GetWorksheetName(firstRow, collectionType);
                var worksheet = package.Workbook.Worksheets[worksheetName];

                if (worksheet == null)
                {
                    continue;
                }

                // loop through the properties in the collection type
                // and see if any have a SpreadsheetLinkAttribute specified
                for (var prop = 1; prop <= properties.Length; prop++)
                {
                    var property = properties.ElementAt(prop - 1);

                    if (property.GetCustomAttribute<SpreadsheetExcludeAttribute>() != null)
                    {
                        continue;
                    }

                    var attribute = property.GetCustomAttribute<SpreadsheetLinkAttribute>();

                    if (attribute == null)
                    {
                        // no SpreadsheetLinkAttribute for this property,
                        // so skip to the next property
                        continue;
                    }

                    // get the worksheet specified by the attribute
                    var linkSheet = package.Workbook.Worksheets[attribute.WorksheetName];

                    if (linkSheet == null)
                    {
                        // if the target worksheet specified by the attribute doesn't exist,
                        // we can't add any links, so just skip to the next property
                        continue;
                    }

                    var linkColumn = string.Empty;

                    // loop through the columns of the first row of the
                    // attribute's target worksheet and see if any
                    // match the attribute's column header value
                    for (var col = 1; col <= linkSheet.Dimension.Columns; col++)
                    {
                        var letter = GetColumnLetter(col);
                        if (linkSheet.Cells[letter + "1"].Value.ToString().Equals(attribute.ColumnHeaderValue))
                        {
                            linkColumn = letter;
                            break;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(linkColumn))
                    {
                        // we found the target column of the target worksheet, so we can add links!
                        AddColumnLinks(worksheet, prop, linkSheet, linkColumn);
                    }
                }
            }
        }

        private static void AddColumnLinks(ExcelWorksheet worksheet, int worksheetColumnIndex, ExcelWorksheet linkSheet, string linkColumn)
        {
            // loop through the cells of the worksheet that correspond to the property
            // that had the SpreadsheetLinkAttribute and try to find a link target for each
            for (var worksheetRow = 1; worksheetRow <= worksheet.Dimension.Rows; worksheetRow++)
            {
                var worksheetCell = worksheet.Cells[$"{GetColumnLetter(worksheetColumnIndex)}{worksheetRow}"];
                var worksheetValue = worksheetCell.Value.ToString();

                // loop through the cells of the target worksheet column and see if any of the values
                // match the value of the current worksheet cell
                for (var linksheetRow = 1; linksheetRow <= linkSheet.Dimension.Rows; linksheetRow++)
                {
                    var linksheetValue = linkSheet.Cells[$"{linkColumn}{linksheetRow}"].Value.ToString();

                    if (worksheetValue.Equals(linksheetValue))
                    {
                        // we found a match! this is the link target,
                        // so add the hyperlink to the worksheet cell
                        // and stop searching for targets for this row
                        worksheetCell.Hyperlink = new ExcelHyperLink($"'{linkSheet.Name}'!{linkColumn}{linksheetRow}", worksheetValue.ToString());
                        worksheetCell.Style.Font.UnderLine = true;
                        worksheetCell.Style.Font.Color.SetColor(Color.Blue);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the Excel-style column letter for the specified numerical index (e.g. 4 is D, 26 is Z, 27 is AA, 28 is AB...).
        /// </summary>
        /// <param name="column">The numerical index of the column.</param>
        /// <returns>The corresponding Excel-style column letter.</returns>
        private static string GetColumnLetter(int column)
        {
            if (column <= Letters.Length)
            {
                return Letters[column - 1].ToString();
            }

            var number = column;
            var letter = string.Empty;

            while (number > 0)
            {
                var remainder = (number - 1) % Letters.Length;
                letter = Letters[remainder] + letter;
                number = (number - remainder) / Letters.Length;
            }

            return letter;
        }

        #endregion
    }
}
