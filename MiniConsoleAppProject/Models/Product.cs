using MiniConsoleAppProject.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniConsoleAppProject.Models
{
    internal class Product :BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
       

        public Product(string name, int price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;
            
            
        }
    }
}
