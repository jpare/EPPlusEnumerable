using System;
using System.Collections.Generic;

namespace SampleConsoleApp
{
    public class SampleDataContext : IDisposable
    {
        #region Properties

        public IEnumerable<User> Users { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        #endregion

        #region Constructors

        public SampleDataContext()
        {
            Users = GetUsers();
            Orders = GetOrders();
        }

        #endregion

        #region Private Methods

        private static IEnumerable<User> GetUsers()
        {
            return new[] {
                new User{ Name = "Jackson Turner", Address = "123 Main St", City = "New York", State = "NY", Zip = "11226"},
                new User{ Name = "Megan Perry", Address = "123 Oak St", City = "Los Angeles", State = "CA", Zip = "90011"},
                new User{ Name = "Ryan Harris", Address = "123 Pine St", City = "Chicago", State = "IL", Zip = "60629"},
                new User{ Name = "Mason Edwards", Address = "123 Maple St", City = "Houston", State = "TX", Zip = "77084"},
                new User{ Name = "Noah Jenkins", Address = "123 Cedar St", City = "Philadelphia", State = "PA", Zip = "19103"},
                new User{ Name = "Stephanie Hayes", Address = "123 Elm St", City = "Phoenix", State = "AZ", Zip = "85001"},
                new User{ Name = "Caleb Scott", Address = "123 View St", City = "San Antonio", State = "TX", Zip = "78201"},
                new User{ Name = "Morgan Wood", Address = "123 Washington St", City = "Dallas", State = "TX", Zip = "75217"},
                new User{ Name = "James Parker", Address = "123 Lake St", City = "San Jose", State = "CA", Zip = "95113"},
                new User{ Name = "Austin Jackson", Address = "123 Hill St", City = "Indianapolis", State = "IN", Zip = "46268"},
            };
        }

        private static IEnumerable<Order> GetOrders()
        {
            return new[] {
                new Order{ Number = 1, Item = "Water Filter", Customer = "Jackson Turner", Price = (decimal)13.69, Date= DateTime.Now},
                new Order{ Number = 2, Item = "Digital Scale", Customer = "Jackson Turner", Price = (decimal)13.00, Date= DateTime.Now.AddDays(-1) },
                new Order{ Number = 3, Item = "Ceramic Heater", Customer = "Jackson Turner", Price = (decimal)24.97, Date= DateTime.Now.AddDays(-5)},
                new Order{ Number = 4, Item = "Humidifier", Customer = "Jackson Turner", Price = (decimal)88.79, Date= DateTime.Now.AddDays(-9)},
                new Order{ Number = 5, Item = "Coffee Filters", Customer = "Jackson Turner", Price = (decimal)7.79, Date= DateTime.Now.AddDays(-13)},
                new Order{ Number = 6, Item = "Crock Pot", Customer = "Jackson Turner", Price = (decimal)23.99, Date= DateTime.Now.AddDays(-17)},
                new Order{ Number = 7, Item = "Vacuum Cleaner", Customer = "Jackson Turner", Price = (decimal)79.00, Date= DateTime.Now.AddDays(-21)},
                new Order{ Number = 8, Item = "Coffee Pot", Customer = "Jackson Turner", Price = (decimal)24.92, Date= DateTime.Now.AddDays(-25)},
                new Order{ Number = 9, Item = "TV Stand", Customer = "Jackson Turner", Price = (decimal)25.77, Date= DateTime.Now.AddDays(-29)},
                new Order{ Number = 10, Item = "Electric Steam Mop", Customer = "Jackson Turner", Price = (decimal)79.99, Date= DateTime.Now.AddDays(-33)},

                new Order{ Number = 11, Item = "Water Filter", Customer = "Megan Perry", Price = (decimal)13.69, Date= DateTime.Now},
                new Order{ Number = 12, Item = "Digital Scale", Customer = "Megan Perry", Price = (decimal)13.00, Date= DateTime.Now.AddDays(-1) },
                new Order{ Number = 13, Item = "Ceramic Heater", Customer = "Megan Perry", Price = (decimal)24.97, Date= DateTime.Now.AddDays(-5)},
                new Order{ Number = 14, Item = "Humidifier", Customer = "Megan Perry", Price = (decimal)88.79, Date= DateTime.Now.AddDays(-9)},
                new Order{ Number = 15, Item = "Coffee Filters", Customer = "Megan Perry", Price = (decimal)7.79, Date= DateTime.Now.AddDays(-13)},
                new Order{ Number = 16, Item = "Crock Pot", Customer = "Megan Perry", Price = (decimal)23.99, Date= DateTime.Now.AddDays(-17)},
                new Order{ Number = 17, Item = "Vacuum Cleaner", Customer = "Megan Perry", Price = (decimal)79.00, Date= DateTime.Now.AddDays(-21)},
                new Order{ Number = 18, Item = "Coffee Pot", Customer = "Megan Perry", Price = (decimal)24.92, Date= DateTime.Now.AddDays(-25)},
                new Order{ Number = 19, Item = "TV Stand", Customer = "Megan Perry", Price = (decimal)25.77, Date= DateTime.Now.AddDays(-29)},
                new Order{ Number = 20, Item = "Electric Steam Mop", Customer = "Megan Perry", Price = (decimal)79.99, Date= DateTime.Now.AddDays(-33)},

                new Order{ Number = 21, Item = "Water Filter", Customer = "Ryan Harris", Price = (decimal)13.69, Date= DateTime.Now},
                new Order{ Number = 22, Item = "Digital Scale", Customer = "Ryan Harris", Price = (decimal)13.00, Date= DateTime.Now.AddDays(-1) },
                new Order{ Number = 23, Item = "Ceramic Heater", Customer = "Ryan Harris", Price = (decimal)24.97, Date= DateTime.Now.AddDays(-2)},
                new Order{ Number = 24, Item = "Humidifier", Customer = "Ryan Harris", Price = (decimal)88.79, Date= DateTime.Now.AddDays(-3)},
                new Order{ Number = 25, Item = "Coffee Filters", Customer = "Ryan Harris", Price = (decimal)7.79, Date= DateTime.Now.AddDays(-4)},
                new Order{ Number = 26, Item = "Crock Pot", Customer = "Ryan Harris", Price = (decimal)23.99, Date= DateTime.Now.AddDays(-5)},
                new Order{ Number = 27, Item = "Vacuum Cleaner", Customer = "Ryan Harris", Price = (decimal)79.00, Date= DateTime.Now.AddDays(-6)},
                new Order{ Number = 28, Item = "Coffee Pot", Customer = "Ryan Harris", Price = (decimal)24.92, Date= DateTime.Now.AddDays(-7)},
                new Order{ Number = 29, Item = "TV Stand", Customer = "Ryan Harris", Price = (decimal)25.77, Date= DateTime.Now.AddDays(-8)},
                new Order{ Number = 30, Item = "Electric Steam Mop", Customer = "Ryan Harris", Price = (decimal)79.99, Date= DateTime.Now.AddDays(-9)},

                new Order{ Number = 31, Item = "Water Filter", Customer = "Mason Edwards", Price = (decimal)13.69, Date= DateTime.Now},
                new Order{ Number = 32, Item = "Digital Scale", Customer = "Mason Edwards", Price = (decimal)13.00, Date= DateTime.Now.AddDays(-1) },
                new Order{ Number = 33, Item = "Ceramic Heater", Customer = "Mason Edwards", Price = (decimal)24.97, Date= DateTime.Now.AddDays(-2)},
                new Order{ Number = 34, Item = "Humidifier", Customer = "Mason Edwards", Price = (decimal)88.79, Date= DateTime.Now.AddDays(-3)},
                new Order{ Number = 35, Item = "Coffee Filters", Customer = "Mason Edwards", Price = (decimal)7.79, Date= DateTime.Now.AddDays(-4)},
                new Order{ Number = 36, Item = "Crock Pot", Customer = "Mason Edwards", Price = (decimal)23.99, Date= DateTime.Now.AddDays(-5)},
                new Order{ Number = 37, Item = "Vacuum Cleaner", Customer = "Mason Edwards", Price = (decimal)79.00, Date= DateTime.Now.AddDays(-6)},
                new Order{ Number = 38, Item = "Coffee Pot", Customer = "Mason Edwards", Price = (decimal)24.92, Date= DateTime.Now.AddDays(-7)},
                new Order{ Number = 39, Item = "TV Stand", Customer = "Mason Edwards", Price = (decimal)25.77, Date= DateTime.Now.AddDays(-8)},
                new Order{ Number = 40, Item = "Electric Steam Mop", Customer = "Mason Edwards", Price = (decimal)79.99, Date= DateTime.Now.AddDays(-9)},

                new Order{ Number = 41, Item = "Water Filter", Customer = "Noah Jenkins", Price = (decimal)13.69, Date= DateTime.Now},
                new Order{ Number = 42, Item = "Digital Scale", Customer = "Noah Jenkins", Price = (decimal)13.00, Date= DateTime.Now.AddDays(-1) },
                new Order{ Number = 43, Item = "Ceramic Heater", Customer = "Noah Jenkins", Price = (decimal)24.97, Date= DateTime.Now.AddDays(-2)},
                new Order{ Number = 44, Item = "Humidifier", Customer = "Noah Jenkins", Price = (decimal)88.79, Date= DateTime.Now.AddDays(-3)},
                new Order{ Number = 45, Item = "Coffee Filters", Customer = "Noah Jenkins", Price = (decimal)7.79, Date= DateTime.Now.AddDays(-4)},
                new Order{ Number = 46, Item = "Crock Pot", Customer = "Noah Jenkins", Price = (decimal)23.99, Date= DateTime.Now.AddDays(-5)},
                new Order{ Number = 47, Item = "Vacuum Cleaner", Customer = "Noah Jenkins", Price = (decimal)79.00, Date= DateTime.Now.AddDays(-6)},
                new Order{ Number = 48, Item = "Coffee Pot", Customer = "Noah Jenkins", Price = (decimal)24.92, Date= DateTime.Now.AddDays(-7)},
                new Order{ Number = 49, Item = "TV Stand", Customer = "Noah Jenkins", Price = (decimal)25.77, Date= DateTime.Now.AddDays(-8)},
                new Order{ Number = 50, Item = "Electric Steam Mop", Customer = "Noah Jenkins", Price = (decimal)79.99, Date= DateTime.Now.AddDays(-9)},

                new Order{ Number = 51, Item = "Water Filter", Customer = "Stephanie Hayes", Price = (decimal)13.69, Date= DateTime.Now},
                new Order{ Number = 52, Item = "Digital Scale", Customer = "Stephanie Hayes", Price = (decimal)13.00, Date= DateTime.Now.AddDays(-1) },
                new Order{ Number = 53, Item = "Ceramic Heater", Customer = "Stephanie Hayes", Price = (decimal)24.97, Date= DateTime.Now.AddDays(-2)},
                new Order{ Number = 54, Item = "Humidifier", Customer = "Stephanie Hayes", Price = (decimal)88.79, Date= DateTime.Now.AddDays(-3)},
                new Order{ Number = 55, Item = "Coffee Filters", Customer = "Stephanie Hayes", Price = (decimal)7.79, Date= DateTime.Now.AddDays(-4)},
                new Order{ Number = 56, Item = "Crock Pot", Customer = "Stephanie Hayes", Price = (decimal)23.99, Date= DateTime.Now.AddDays(-5)},
                new Order{ Number = 57, Item = "Vacuum Cleaner", Customer = "Stephanie Hayes", Price = (decimal)79.00, Date= DateTime.Now.AddDays(-6)},
                new Order{ Number = 58, Item = "Coffee Pot", Customer = "Stephanie Hayes", Price = (decimal)24.92, Date= DateTime.Now.AddDays(-7)},
                new Order{ Number = 59, Item = "TV Stand", Customer = "Stephanie Hayes", Price = (decimal)25.77, Date= DateTime.Now.AddDays(-8)},
                new Order{ Number = 60, Item = "Electric Steam Mop", Customer = "Stephanie Hayes", Price = (decimal)79.99, Date= DateTime.Now.AddDays(-9)},

                new Order{ Number = 61, Item = "Water Filter", Customer = "Caleb Scott", Price = (decimal)13.69, Date= DateTime.Now},
                new Order{ Number = 62, Item = "Digital Scale", Customer = "Caleb Scott", Price = (decimal)13.00, Date= DateTime.Now.AddDays(-1) },
                new Order{ Number = 63, Item = "Ceramic Heater", Customer = "Caleb Scott", Price = (decimal)24.97, Date= DateTime.Now.AddDays(-2)},
                new Order{ Number = 64, Item = "Humidifier", Customer = "Caleb Scott", Price = (decimal)88.79, Date= DateTime.Now.AddDays(-3)},
                new Order{ Number = 65, Item = "Coffee Filters", Customer = "Caleb Scott", Price = (decimal)7.79, Date= DateTime.Now.AddDays(-4)},
                new Order{ Number = 66, Item = "Crock Pot", Customer = "Caleb Scott", Price = (decimal)23.99, Date= DateTime.Now.AddDays(-5)},
                new Order{ Number = 67, Item = "Vacuum Cleaner", Customer = "Caleb Scott", Price = (decimal)79.00, Date= DateTime.Now.AddDays(-6)},
                new Order{ Number = 68, Item = "Coffee Pot", Customer = "Caleb Scott", Price = (decimal)24.92, Date= DateTime.Now.AddDays(-7)},
                new Order{ Number = 69, Item = "TV Stand", Customer = "Caleb Scott", Price = (decimal)25.77, Date= DateTime.Now.AddDays(-8)},
                new Order{ Number = 70, Item = "Electric Steam Mop", Customer = "Caleb Scott", Price = (decimal)79.99, Date= DateTime.Now.AddDays(-9)},

                new Order{ Number = 71, Item = "Water Filter", Customer = "Morgan Wood", Price = (decimal)13.69, Date= DateTime.Now},
                new Order{ Number = 72, Item = "Digital Scale", Customer = "Morgan Wood", Price = (decimal)13.00, Date= DateTime.Now.AddDays(-1) },
                new Order{ Number = 73, Item = "Ceramic Heater", Customer = "Morgan Wood", Price = (decimal)24.97, Date= DateTime.Now.AddDays(-2)},
                new Order{ Number = 74, Item = "Humidifier", Customer = "Morgan Wood", Price = (decimal)88.79, Date= DateTime.Now.AddDays(-3)},
                new Order{ Number = 75, Item = "Coffee Filters", Customer = "Morgan Wood", Price = (decimal)7.79, Date= DateTime.Now.AddDays(-4)},
                new Order{ Number = 76, Item = "Crock Pot", Customer = "Morgan Wood", Price = (decimal)23.99, Date= DateTime.Now.AddDays(-5)},
                new Order{ Number = 77, Item = "Vacuum Cleaner", Customer = "Morgan Wood", Price = (decimal)79.00, Date= DateTime.Now.AddDays(-6)},
                new Order{ Number = 78, Item = "Coffee Pot", Customer = "Morgan Wood", Price = (decimal)24.92, Date= DateTime.Now.AddDays(-7)},
                new Order{ Number = 79, Item = "TV Stand", Customer = "Morgan Wood", Price = (decimal)25.77, Date= DateTime.Now.AddDays(-8)},
                new Order{ Number = 80, Item = "Electric Steam Mop", Customer = "Morgan Wood", Price = (decimal)79.99, Date= DateTime.Now.AddDays(-9)},

                new Order{ Number = 81, Item = "Water Filter", Customer = "James Parker", Price = (decimal)13.69, Date= DateTime.Now},
                new Order{ Number = 82, Item = "Digital Scale", Customer = "James Parker", Price = (decimal)13.00, Date= DateTime.Now.AddDays(-1) },
                new Order{ Number = 83, Item = "Ceramic Heater", Customer = "James Parker", Price = (decimal)24.97, Date= DateTime.Now.AddDays(-2)},
                new Order{ Number = 84, Item = "Humidifier", Customer = "James Parker", Price = (decimal)88.79, Date= DateTime.Now.AddDays(-3)},
                new Order{ Number = 85, Item = "Coffee Filters", Customer = "James Parker", Price = (decimal)7.79, Date= DateTime.Now.AddDays(-4)},
                new Order{ Number = 86, Item = "Crock Pot", Customer = "James Parker", Price = (decimal)23.99, Date= DateTime.Now.AddDays(-5)},
                new Order{ Number = 87, Item = "Vacuum Cleaner", Customer = "James Parker", Price = (decimal)79.00, Date= DateTime.Now.AddDays(-6)},
                new Order{ Number = 88, Item = "Coffee Pot", Customer = "James Parker", Price = (decimal)24.92, Date= DateTime.Now.AddDays(-7)},
                new Order{ Number = 89, Item = "TV Stand", Customer = "James Parker", Price = (decimal)25.77, Date= DateTime.Now.AddDays(-8)},
                new Order{ Number = 90, Item = "Electric Steam Mop", Customer = "James Parker", Price = (decimal)79.99, Date= DateTime.Now.AddDays(-9)},

                new Order{ Number = 91, Item = "Water Filter", Customer = "Austin Jackson", Price = (decimal)13.69, Date= DateTime.Now},
                new Order{ Number = 92, Item = "Digital Scale", Customer = "Austin Jackson", Price = (decimal)13.00, Date= DateTime.Now.AddDays(-1) },
                new Order{ Number = 93, Item = "Ceramic Heater", Customer = "Austin Jackson", Price = (decimal)24.97, Date= DateTime.Now.AddDays(-2)},
                new Order{ Number = 94, Item = "Humidifier", Customer = "Austin Jackson", Price = (decimal)88.79, Date= DateTime.Now.AddDays(-3)},
                new Order{ Number = 95, Item = "Coffee Filters", Customer = "Austin Jackson", Price = (decimal)7.79, Date= DateTime.Now.AddDays(-4)},
                new Order{ Number = 96, Item = "Crock Pot", Customer = "Austin Jackson", Price = (decimal)23.99, Date= DateTime.Now.AddDays(-5)},
                new Order{ Number = 97, Item = "Vacuum Cleaner", Customer = "Austin Jackson", Price = (decimal)79.00, Date= DateTime.Now.AddDays(-6)},
                new Order{ Number = 98, Item = "Coffee Pot", Customer = "Austin Jackson", Price = (decimal)24.92, Date= DateTime.Now.AddDays(-7)},
                new Order{ Number = 99, Item = "TV Stand", Customer = "Austin Jackson", Price = (decimal)25.77, Date= DateTime.Now.AddDays(-8)},
                new Order{ Number = 100, Item = "Electric Steam Mop", Customer = "Austin Jackson", Price = (decimal)79.99, Date= DateTime.Now.AddDays(-9)},
            };
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            Users = null;
            Orders = null;
        }

        #endregion
    }
}