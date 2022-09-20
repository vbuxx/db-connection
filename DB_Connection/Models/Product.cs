using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Connection.Models
{
    class Product
    {
        public Product (string name, int stock, int price)
        {
            Name = name;
            Stock = stock;
            Price = price;
        }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }


    }
}
