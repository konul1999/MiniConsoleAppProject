using MiniConsoleAppProject.Models;
using MiniConsoleAppProject.Repositories;
using MiniConsoleAppProject.Utilities.Enums;

namespace MiniConsoleAppProject.Services
{
    internal class OrderService
    {
        private readonly string _proPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Data", "Products.json");
        private readonly string _orPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Data", "Orders.json");

        public Repository<Product> ProductRepository { get; set; } = new Repository<Product>();
        public Repository<Order> OrderRepository { get; set; } = new Repository<Order>();

        public void OrderProduct()
        {
            Console.WriteLine("Enter your Email:");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                Console.WriteLine("Invalid email!");
                return;
            }

            List<Product> products = ProductRepository.Deserialize(_proPath);
            if (products.Count == 0)
            {
                Console.WriteLine("No product found");
                return;
            }

            foreach (var item in products)
                item.PrintInfo();

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
                string choice = Console.ReadLine().Trim().ToLower();

                if (choice == "y" || choice == "yes")
                    continue;
                else if (choice == "n" || choice == "no")
                    break;
                else
                    Console.WriteLine("Invalid input! Please enter 'y' or 'n'.");
            }

            if (orderItems.Count == 0)
            {
                Console.WriteLine("No items selected. Order canceled.");
                return;
            }

            Order order = new Order(email, orderItems);

            List<Order> orders = OrderRepository.Deserialize(_orPath);
            orders.Add(order);

            ProductRepository.Serialize(_proPath, products);
            OrderRepository.Serialize(_orPath, orders);

            Console.WriteLine($"Order created successfully! Total: {order.Total}");


        }
    }
}
