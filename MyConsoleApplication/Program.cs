using DomainModel;
using log4net;
using ServiceLayer;
using ServiceLayer.ServiceImplementation;
using ServiceLayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyConsoleApplication
{
    class Program
    {

        static void Main(string[] args)
        {
            // addUser();
            addProduct();
            //addCategory();
           // listAllCategories();

          
        }

        private static void addProduct()
        {
            IProductServices productServices = new ProductServicesImplementation();
            User user = new User
            {
                Id = 1,
                Name = "Apriotese Andreea",
                Status = "Activ",

            };
           Money money = new Money
            {
                Amount = 10000,
                Currency = "RON"
            };
            Category category = new Category
            {
               Id = 2,
                Name = "Masini",
            };

            Product product = new Product
            {
                Name = "Masina de colectie",
                Description = "licitez o masina din anul 2000",
                OwnerUser = user,
                EndDate = new DateTime(2022, 12, 31),
                StartDate  = DateTime.Now,
                StartingPrice = money,
                Category = category,
                Status = "Opened",
            };
                
            productServices.AddProduct(product);

        }

        private static void addCategory()
        {
            ICategoryServices service = new CategoryServicesImplementation();
            Category category = new Category();
            category.Name = "Bijuterii";
            service.AddCategory(category);

        
        }
        private static void addUser()
        {
            IUserServices userServices = new UserServicesImplementation();
            User user = new User
            {
                Name = "Filip Diana Estera",
                Status = "Activ",

            };
            userServices.AddUser(user);
           
        }
        private static void listAllProducts()
        {
            IProductServices service = new ProductServicesImplementation();
            foreach (var item in service.GetListOfProducts())
            {
                Console.WriteLine(item.Name);
            }
        }
        private static void listAllCategories()
        {
            ICategoryServices service = new CategoryServicesImplementation();
            foreach (var item in service.GetListOfCategories())
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
