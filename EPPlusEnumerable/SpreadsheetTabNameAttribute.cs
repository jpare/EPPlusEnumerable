using System;

namespace EPPlusEnumerable
{
    /// <summary>
    /// Use this attribute to denote that the property is to be used as the name of the Excel worksheet.
    /// A format string may optionally be supplied.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SpreadsheetTabNameAttribute : Attribute
    {
        #region Properties

        public string FormatString { get; set; }

        #endregion

        #region Constructors

        public SpreadsheetTabNameAttribute()
        {
            FormatString = null;
        }

        #endregion
    }
}
