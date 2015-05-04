using EPPlusEnumerable;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new List<IEnumerable<object>>();

            using (var db = new SampleDataContext())
            {
                data.Add(db.Users.OrderBy(x => x.Name).ToList());
                data.Add(db.Orders.OrderBy(x => x.Date).GroupBy(x => x.Date.Month));
            }

            var bytes = Spreadsheet.Create(data);
            File.WriteAllBytes("MySpreadsheet.xlsx", bytes);
        }
    }

    [DisplayName("Customers")]
    public class User
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }
    }

    [DisplayName("Orders"), SpreadsheetTableStyle(TableStyles.Medium16)]
    public class Order
    {
        public int Number { get; set; }

        public string Item { get; set; }

        [SpreadsheetLink("Customer", "Name")]
        public string Customer { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }

        [SpreadsheetTabName(FormatString = "{0:MMMM}")]
        public DateTime Date { get; set; }
    }
}
