using MiniConsoleAppProject.Models;
using MiniConsoleAppProject.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace MiniConsoleAppProject.Services
{
    internal class ProductService
    {
        public readonly string _path= @"C:\Users\ASUS\Desktop\MiniConsoleAppProject\MiniConsoleAppProject\Data\Products.json";

        public Repository<Product> ProductRepository {  get; set; } = new Repository<Product>();

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

            if (name is null) return;

          List<Product> products =  ProductRepository.Deserialize(_path);
            
            
            bool isDublicate=products.Any(p => p.Name == name);
            if (isDublicate)
            {
                Console.Clear();
                Console.WriteLine($"Order name {name} already exist");
                return; 
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





            string json = JsonConvert.SerializeObject(order);

            
            //Products.Add(Product);
            //ProductRepository.Serialize(_path,Products)




        }
          

      
    }
}
