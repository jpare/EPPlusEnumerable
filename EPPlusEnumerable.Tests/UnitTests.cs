using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleConsoleApp;
using System.Linq;
using System.Collections.Generic;
using EPPlusEnumerable;

namespace EPPlusEnumerable.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void BasicFuntionality()
        {
            using (var db = new SampleDataContext())
            {
                var data = db.Users;
                var package = Spreadsheet.CreatePackage(data);
                Assert.IsTrue(package.Workbook.Worksheets.Count == 1);
                Assert.IsTrue(package.Workbook.Worksheets[1].Name == "Customers");
                Assert.IsTrue(package.Workbook.Worksheets[1].Dimension.End.Row - 1 == data.Count());
            }
        }

        [TestMethod]
        public void WorksheetNameAttribute()
        {
            using (var db = new SampleDataContext())
            {
                var data = db.Orders.OrderBy(x => x.Date).GroupBy(x => x.Date.Month);
                var package = Spreadsheet.CreatePackage(data);
                Assert.IsTrue(package.Workbook.Worksheets.Count > 1);
                Assert.IsTrue(package.Workbook.Worksheets[package.Workbook.Worksheets.Count].Name == DateTime.Now.AddDays(-1).ToString("MMMM"));
            }
        }       
    }
}
