using MiniConsoleAppProject.Models;
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
        public void Run()
        {
            int num = 0;
            string str = null;
            bool result = false;

            while (!(num == 0 && result))
            {
                Console.WriteLine("1.Create Product\n2.Delete Product\n3.Get Product By Id\n4.Show All Product\n5.Refill Product\n6.Order Product\n7.Show All Orders\n8.Change Order Status\n\n0.Exit ");
                str = Console.ReadLine();
                Console.Clear();
                result = int.TryParse(str, out num);

                switch (num)
                {
                    case 1:
                        
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
                    case 6:
                        Console.WriteLine("Product Ordered");
                        break;
                    case 7:
                        Console.WriteLine("All Orders List");
                        break;
                    case 8:
                        Console.WriteLine("Order Status changed");
                        break;
                    case 0:
                        if(result)
                        {
                            Console.WriteLine("Program ended");
                        }
                        else
                        {
                            Console.WriteLine("Wrong input");
                        }
                        break;
                    default:
                        Console.WriteLine("Wrong input. Please,try again");
                        break;

                }
            }
        }
    }
}
