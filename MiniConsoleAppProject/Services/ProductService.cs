using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniConsoleAppProject.Services
{
    internal class ProductService
    {
        public void GetName()
        {

        } 
        public void GetPrice()
        {

        }
        public void GetStock()
        {

        }




        public void CreateProduct()
        {
            Console.WriteLine("Please, choose Product Name:");
            string name = Console.ReadLine().Trim();

            for (int i = 0; i < name.Length; i++)
            {
                if (!char.IsLetter(name[i]))
                {
                    return;
                }
            }



            











            Console.WriteLine("Product created");
        }
    }
}
