using MiniConsoleAppProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace MiniConsoleAppProject.Services
{
    internal class ProductService
    {
        public readonly string _path= @"C:\Users\ASUS\Desktop\MiniConsoleAppProject\MiniConsoleAppProject\Data\Products.json";

        private string GetName()
        {
            
            Console.WriteLine("Please, choose Product Name:");
            string name = Console.ReadLine().Trim();

            for (int i = 0; i < name.Length; i++)
            {
                if (!char.IsLetter(name[i]))
                {
                    return null;
                }
                return name;
            }
           


            Console.WriteLine("Product created");
           
            using (StreamReader sr = new (_path))
            { 
                sr.ReadToEnd();
            }
            string json = JsonConvert.SerializeObject(product);

            using (StreamWriter sw = new StreamWriter(_path))
            {
                sw.Write(json);
            }

        }
        
        private void GetPrice()
        {

        }
        private void GetStock()
        {

        }




        public void CreateProduct()
        {
           GetName();



            Console.Clear();
            Console.WriteLine("Product created");

            
                
            
            
            
        }

      
    }
}
