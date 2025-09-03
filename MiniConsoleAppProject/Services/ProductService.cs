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
        private readonly string _productPath = @"C:\Users\ASUS\Desktop\MiniConsoleAppProject\MiniConsoleAppProject\Data\Products.json";
        private readonly string _orderPath = @"C:\Users\ASUS\Desktop\MiniConsoleAppProject\MiniConsoleAppProject\Data\Products.json";

        public Repository<Product> ProductRepository { get; set; } = new Repository<Product>();
        public Repository<Order> OrderRepository { get; set; } = new Repository<Order>();

        #region Product Methods
        public void CreateProduct()
        {
            string name = GetName();
            decimal price = GetPrice();
            int stock = GetStock();

            Product newProduct = new Product(name, price, stock);

            List<Product> products = ProductRepository.Deserialize(_productPath);
            products.Add(newProduct);
            ProductRepository.Serialize(_productPath, products);

            Console.Clear();
            Console.WriteLine($"Product '{name}' created successfully!");
        }

        public void DeleteProduct()
        {
            Console.WriteLine("Enter Product ID to delete:");
            string input = Console.ReadLine();

            List<Product> products = ProductRepository.Deserialize(_productPath);
            Product product = products.FirstOrDefault(p => p.Id.ToString() == input);

            if (product == null)
            {
                Console.WriteLine("Product not found!");
                return;
            }

            products.Remove(product);
            ProductRepository.Serialize(_productPath, products);
            Console.WriteLine($"Product '{product.Name}' deleted successfully!");
        }

        public void GetProductById()
        {
            Console.WriteLine("Enter Product ID:");
            string input = Console.ReadLine();

            List<Product> products = ProductRepository.Deserialize(_productPath);
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
            List<Product> products = ProductRepository.Deserialize(_productPath);

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
            Console.WriteLine("Enter Product ID to refill:");
            string input = Console.ReadLine();

            List<Product> products = ProductRepository.Deserialize(_productPath);
            Product product = products.FirstOrDefault(p => p.Id.ToString() == input);

            if (product == null)
            {
                Console.WriteLine("Product not found!");
                return;
            }

            Console.WriteLine($"Current Stock: {product.Stock}. Enter amount to add:");
            if (!int.TryParse(Console.ReadLine(), out int add) || add < 0)
            {
                Console.WriteLine("Invalid amount!");
                return;
            }

            product.Stock += add;
            ProductRepository.Serialize(_productPath, products);
            Console.WriteLine($"Product '{product.Name}' stock updated to {product.Stock}");
        }
        #endregion

        #region Order Methods
        public void OrderProduct()
        {
            Console.WriteLine("Enter your Email:");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                Console.WriteLine("Invalid email!");
                return;
            }

            List<Product> products = ProductRepository.Deserialize(_productPath);
            List<OrderItem> orderItems = new List<OrderItem>();

            while (true)
            {
                Console.WriteLine("Enter Product ID to order (or 'done' to finish):");
                string input = Console.ReadLine();
                if (input.ToLower() == "done") break;

                Product product = products.FirstOrDefault(p => p.Id.ToString() == input);
                if (product == null)
                {
                    Console.WriteLine("Product not found!");
                    continue;
                }

                Console.WriteLine($"Enter quantity for '{product.Name}' (Stock: {product.Stock}):");
                if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
                {
                    Console.WriteLine("Invalid quantity!");
                    continue;
                }

                if (qty > product.Stock)
                {
                    Console.WriteLine("Not enough stock!");
                    continue;
                }

                orderItems.Add(new OrderItem(product, qty));
                product.Stock -= qty;

                Console.WriteLine("Do you want to add another product? (y/n)");
                string choice = Console.ReadLine().ToLower();
                if (choice != "y") break;
            }

            if (orderItems.Count == 0)
            {
                Console.WriteLine("No items selected. Order canceled.");
                return;
            }

            Order order = new Order(email);
            foreach (var item in orderItems)
                order.AddItem(item.Product, item.Count);

            List<Order> orders = OrderRepository.Deserialize(_orderPath);
            orders.Add(order);
            OrderRepository.Serialize(_orderPath, orders);
            ProductRepository.Serialize(_productPath, products);

            Console.WriteLine($"Order created successfully! Total: {order.Total}");
        }

        public void ShowAllOrders()
        {
            List<Order> orders = OrderRepository.Deserialize(_orderPath);

            if (orders.Count == 0)
            {
                Console.WriteLine("No orders found!");
                return;
            }

            foreach (var order in orders)
                order.PrintInfo();
        }

        public void ChangeOrderStatus()
        {
            Console.WriteLine("Enter Order ID to change status:");
            string input = Console.ReadLine();

            List<Order> orders = OrderRepository.Deserialize(_orderPath);
            Order order = orders.FirstOrDefault(o => o.Id.ToString() == input);

            if (order == null)
            {
                Console.WriteLine("Order not found!");
                return;
            }

            Console.WriteLine("Select new status: 1.Pending  2.Confirmed  3.Completed");
            if (!int.TryParse(Console.ReadLine(), out int statusInt) || statusInt < 1 || statusInt > 3)
            {
                Console.WriteLine("Invalid status!");
                return;
            }

            order.ChangeStatus((OrderStatus)statusInt);
            OrderRepository.Serialize(_orderPath, orders);
            Console.WriteLine($"Order status updated to {order.Status}");
        }
        #endregion

        #region Helper Methods
        private string GetName()
        {
            List<Product> products = ProductRepository.Deserialize(_productPath);

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

