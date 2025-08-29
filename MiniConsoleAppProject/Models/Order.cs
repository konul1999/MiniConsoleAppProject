using MiniConsoleAppProject.Models.Base;
using MiniConsoleAppProject.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniConsoleAppProject.Models
{
    internal class Order:BaseEntity
    {
        public List<OrderItem> Items { get; } = new List<OrderItem>();
        public decimal Total {  get; set; }
        public string Email { get; }
        public OrderStatus Status { get; }
        public DateTime OrderedAt { get; }

       
        public Order(OrderStatus status)
        {
            Status = status;
        }
    }

}
