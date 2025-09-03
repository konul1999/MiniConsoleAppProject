using MiniConsoleAppProject.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniConsoleAppProject.Models
{
    internal class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Product(string name, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Warning: Name is empty.");
            }
            Name = name;

            if (price <= 0)
            {
                Console.WriteLine($"Warning: Invalid price ({price}).");
            }
            Price = price;

            if (stock < 0)
            {
                Console.WriteLine($"Warning: Stock cannot be negative ({stock}).");
            }
            Stock = stock;
        }

        public void PrintInfo()
        {
            string stockStatus = Stock <= 0 ? "Out of Stock" : "";
            Console.WriteLine($"ID: {Id}\nName: {Name}\nPrice: {Price:C}\nStock: {Stock} {stockStatus}\n");
        }
    }
}