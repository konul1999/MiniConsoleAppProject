using MiniConsoleAppProject.Models;
using MiniConsoleAppProject.Repositories;
using MiniConsoleAppProject.Utilities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace MiniConsoleAppProject.Services
{
    internal class ProductService
    {
        private readonly string _proPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Data", "Products.json");

        public Repository<Product> ProductRepository { get; set; } = new Repository<Product>();
       

        public void CreateProduct()
        {
            string name = GetName();
            decimal price = GetPrice();
            int stock = GetStock();

            Product newProduct = new Product(name, price, stock);

            List<Product> products = ProductRepository.Deserialize(_proPath);
            products.Add(newProduct);
            ProductRepository.Serialize(_proPath, products);

            Console.Clear();
            Console.WriteLine($"Product '{name}' created successfully!");
        }

        public void DeleteProduct()
        {
            Console.WriteLine("Enter Product ID to delete:");
            string input = Console.ReadLine();

            List<Product> products = ProductRepository.Deserialize(_proPath);
            Product product = products.FirstOrDefault(p => p.Id.ToString() == input);

            if (product == null)
            {
                Console.WriteLine("Product not found!");
                return;
            }

            products.Remove(product);
            ProductRepository.Serialize(_proPath, products);
            Console.WriteLine($"Product '{product.Name}' deleted successfully!");
        }

        public void GetProductById()
        {
            Console.WriteLine("Enter Product ID:");
            string input = Console.ReadLine();

            List<Product> products = ProductRepository.Deserialize(_proPath);
            Product product = products.FirstOrDefault(p => p.Id.ToString() == input);

            if (product == null)
            {
                Console.WriteLine("Product not found!");
                return;
            }

            product.PrintInfo();
        }

        public void ShowAllProducts()
        {
            List<Product> products = ProductRepository.Deserialize(_proPath);

            if (products.Count == 0)
            {
                Console.WriteLine("No products found!");
                return;
            }

            foreach (var product in products)
            {
                string stockStatus = product.Stock <= 0 ? "Out of Stock" : "";
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock} {stockStatus}");
            }
        }

        public void RefillProduct()
        {
            ShowAllProducts();
            Console.WriteLine("Enter Product ID to refill:");
            string input = Console.ReadLine();

            List<Product> products = ProductRepository.Deserialize(_proPath);
            Product product = products.FirstOrDefault(p => p.Id.ToString() == input);

            if (product == null)
            {
                Console.WriteLine("Product not found!");
                return;
            }

            Console.WriteLine($"Current Stock: {product.Stock}. Enter amount to add:");
            if (!int.TryParse(Console.ReadLine(), out int add) || add <= 0)
            {
                Console.WriteLine("Invalid amount!");
                return;
            }

            product.Stock += add;
            ProductRepository.Serialize(_proPath, products);
            Console.WriteLine($"Product '{product.Name}' stock updated to {product.Stock}");
        }

        #region Helper Methods
        private string GetName()
        {
            List<Product> products = ProductRepository.Deserialize(_proPath);

            while (true)
            {
                Console.WriteLine("Enter Product Name:");
                string name = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name cannot be empty!");
                    continue;
                }

                if (products.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Product name already exists!");
                    continue;
                }

                return name;
            }
        }

        private decimal GetPrice()
        {
            while (true)
            {
                Console.WriteLine("Enter Product Price:");
                if (decimal.TryParse(Console.ReadLine(), out decimal price) && price > 0)
                    return price;

                Console.WriteLine("Invalid price!");
            }
        }

        private int GetStock()
        {
            while (true)
            {
                Console.WriteLine("Enter Product Stock:");
                if (int.TryParse(Console.ReadLine(), out int stock) && stock >= 0)
                    return stock;

                Console.WriteLine("Invalid stock!");
            }
        }
        #endregion
    }


}

