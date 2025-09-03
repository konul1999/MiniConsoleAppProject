using MiniConsoleAppProject.Models;
using MiniConsoleAppProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MiniConsoleAppProject
{
    internal class ManagementApp
    {
        private ProductService ProductService { get; set; }
        private OrderService OrderService { get; set; }

        public ManagementApp()
        {
            ProductService = new ProductService();
            OrderService = new OrderService();
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n--- Menu ---");
                Console.WriteLine("1. Create Product");
                Console.WriteLine("2. Delete Product");
                Console.WriteLine("3. Get Product By Id");
                Console.WriteLine("4. Show All Products");
                Console.WriteLine("5. Refill Product");
                Console.WriteLine("6. Order Product");
                Console.WriteLine("7. Show All Orders");
                Console.WriteLine("8. Change Order Status");
                Console.WriteLine("0. Exit");
                Console.Write("Select option: ");

                string input = Console.ReadLine();
                Console.Clear();

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }

                switch (choice)
                {
                    case 1: ProductService.CreateProduct(); break;
                    case 2: ProductService.DeleteProduct(); break;
                    case 3: ProductService.GetProductById(); break;
                    case 4: ProductService.ShowAllProducts(); break;
                    case 5: ProductService.RefillProduct(); break;
                    case 6: OrderService.OrderProduct(); break;
                    case 7: OrderService.ShowAllOrders(); break;
                    case 8: OrderService.ChangeOrderStatus(); break;
                    case 0:
                        Console.WriteLine("Program exited.");
                        return;
                    default:
                        Console.WriteLine("Wrong input. Try again.");
                        break;
                }
            }
        }
    }
}