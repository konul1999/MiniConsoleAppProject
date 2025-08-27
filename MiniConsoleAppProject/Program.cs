using System.Threading.Channels;

namespace MiniConsoleAppProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            int num = 0;
            string str = null;
            bool result = false;

            while (!(num==0 && result))
            {
                Console.WriteLine("1.Create Product\n2.Delete Product\n3.Get Product By Id\n4.Show All Product\n5.Refill Product\n\n0.Exit ");
                str = Console.ReadLine();
                Console.Clear();
                result = int.TryParse(str, out num);
                switch (num)
                {
                    case 1:
                        Console.WriteLine("Product created");
                        break;
                    case 2:
                        Console.WriteLine("Product deleted");
                        break;
                    case 3:
                        Console.WriteLine("List by Product Id");
                        break;
                    case 4:
                        Console.WriteLine("Products list");
                        break;
                    case 5:
                        Console.WriteLine("Product refilled");
                        break;

                    default:
                        Console.WriteLine("Wrong input. Please,try again");

                        break;

                }
            }
            

        }
    }
}
