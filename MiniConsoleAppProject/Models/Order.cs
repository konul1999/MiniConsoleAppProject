using MiniConsoleAppProject.Models.Base;
using MiniConsoleAppProject.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniConsoleAppProject.Models
{
    internal class Order : BaseEntity
    {
        public List<OrderItem> Items { get; } = new List<OrderItem>();
        public decimal Total => Items.Sum(i => i.SubTotal);
        public string Email { get; }
        public OrderStatus Status { get; private set; }
        public DateTime OrderedAt { get; }

        public Order(string email, List<OrderItem> list)
        {
            Items = new List<OrderItem>(list); 
            Email = email;
            Status = OrderStatus.Pending;
            OrderedAt = DateTime.Now;
        }

        public void AddItem(Product product, int count)
        {
            if (Email == null)
            {
                Console.WriteLine("Cannot add items: Order email is invalid.");
                return;
            }

            if (product == null)
            {
                Console.WriteLine("Warning: Cannot add null product. Item not added.");
                return;
            }

            if (count <= 0)
            {
                Console.WriteLine($"Warning: Invalid count ({count}) for product '{product.Name}'. Item not added.");
                return;
            }

            var item = new OrderItem(product, count);
            Items.Add(item);
        }

        public void ChangeStatus(OrderStatus newStatus)
        {
            Status = newStatus;
        }

        public void PrintInfo()
        {
            if (Email == null)
            {
                Console.WriteLine("Cannot print order info: Invalid email.");
                return;
            }

            Console.WriteLine($"Order ID: {Id}\nEmail: {Email}\nStatus: {Status}\nOrdered At: {OrderedAt}\nTotal: {Total:C}");
            foreach (var item in Items)
            {
                Console.WriteLine($"- {item.Product.Name} | Price: {item.Price:C} | Count: {item.Count} | SubTotal: {item.SubTotal:C}");
            }
            Console.WriteLine();
        }
    }
}
