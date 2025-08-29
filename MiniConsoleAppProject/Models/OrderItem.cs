using MiniConsoleAppProject.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniConsoleAppProject.Models
{
    internal class OrderItem :BaseEntity
    {
        public Product Product { get; }
        public int Count { get; }
        public decimal Price { get; }
        public decimal SubTotal { get; }

        public OrderItem()
        {

        }


    }
}


