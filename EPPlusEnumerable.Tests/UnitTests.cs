namespace EPPlusEnumerable.Tests
{
    using System;
    using System.Linq;

    using SampleConsoleApp;

    using Xunit;

    public class UnitTests
    {
        [Fact]
        public void BasicFuntionality()
        {
            using (var db = new SampleDataContext())
            {
                var data = db.Users;
                var package = Spreadsheet.CreatePackage(data);
                Assert.True(package.Workbook.Worksheets.Count == 1);
                Assert.True(package.Workbook.Worksheets[1].Name == "Customers");
                Assert.True(package.Workbook.Worksheets[1].Dimension.End.Row - 1 == data.Count());
            }
        }

        [Fact]
        public void WorksheetNameAttribute()
        {
            using (var db = new SampleDataContext())
            {
                var data = db.Orders.OrderBy(x => x.Date).GroupBy(x => x.Date.Month);
                var package = Spreadsheet.CreatePackage(data);
                Assert.True(package.Workbook.Worksheets.Count > 1);
                Assert.True(package.Workbook.Worksheets[package.Workbook.Worksheets.Count].Name == DateTime.Now.AddDays(-1).ToString("MMMM"));
            }
        }       
    }
}