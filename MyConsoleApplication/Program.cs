using DataMapper.SqlServerDAO;
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
             //addUser();
           // addProduct();
           // addUserAuction();
            //addCategory();
            // listAllCategories();


        }

       /* private static void addProduct()
        {
            // IProductServices productServices = new ProductServicesImplementation();
            User user = new User
            {
                Id = 1,
                Name = "Apriotese Andreea",
                Status = "Activ",

            };
            Money money = new Money
            {
                Amount = 20000,
                Currency = "RON"
            };
            Category category = new Category
            {
                Id = 3,
                Name = "Bijuterii",
            };

            Product product = new Product
            {
                Id = 23,
                Name = "test update 3Ian",
                Description = "test1234",
                OwnerUser = user,
                EndDate = new DateTime(2023, 12, 21),
                StartDate = DateTime.Now,
                StartingPrice = money,
                Category = category,
                Status = "Open"

            };

            // productServices.UpdateProduct(product);


        }

        private static void addUserAuction()
        {
            IUserAuctionServices userAuctionServices = new UserAuctionServicesImplementation();
            Money money = new Money
            {
                Amount = 25000,
                Currency = "RON"
            };
            UserAuction userAuction = new UserAuction
            {
                Product = 23,
                User = 1,
                Price = money,

            };
            userAuctionServices.AddUserAuction(userAuction);

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
                Id = 3,
                Name = "Dinu Ionut-Alin",
                Status = "Inactiv",

            };
            userServices.UpdateUser(user);

        }
        private static void listAllProducts()
        {
            IProductServices service = new ProductServicesImplementation(new SQLProductDataServices(), new SQLConfigurationDataServices());
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
        }*/
    }
}
