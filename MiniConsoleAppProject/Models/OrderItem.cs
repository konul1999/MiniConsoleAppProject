using MiniConsoleAppProject.Models.Base;

namespace MiniConsoleAppProject.Models
{
    internal class OrderItem : BaseEntity
    {

        public Product Product { get; }
        public int Count { get; }
        public decimal Price => Product?.Price ?? 0;
        public decimal SubTotal => Price * Count;

        public OrderItem(Product product, int count)
        {
            if (product == null)
            {
                Console.WriteLine("Warning: Product is null. Item not created.");
                Product = null;
                Count = 0;
                return;
            }

            Product = product;

            if (count <= 0)
            {
                Console.WriteLine($"Warning: Invalid count ({count}) for product '{product.Name}'. Setting count = 1.");
                Count = 1;
            }
            else
            {
                Count = count;
            }
        }
    }
}


