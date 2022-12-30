using DataMapper;
using DataMapper.SqlServerDAO;
using DomainModel;
using ServiceLayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace ServiceLayer.ServiceImplementation
{
    public class ProductServicesImplementation : IProductServices
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductServicesImplementation));
        IProductDataServices productDataServices = new SQLProductDataServices();

        public void AddProduct(Product product)
        {
            log.Info("In AddProduct method");
            var userProducts = productDataServices.GetProductsByUserId(product.OwnerUser.Id);
            if(userProducts != null)
            {
                log.Info("User products have been found.");

                var hasFound = false;
                foreach (var item in userProducts)
                {
                    
                    var distance = StringDistance.LevenshteinDistance(item.Description, product.Description);
                    var currentItem = item;
                    if (distance < 20)
                    {
                        log.Info("A similar product has been found. We won't add the product.");

                        hasFound = true;
                        break;
                    }
                }
                if (!hasFound)
                {
                    productDataServices.AddProduct(product);
                    log.Info("The new product was added!");

                }
                else
                {
                    throw new SimilarDescriptionException(product.Name);
                    log.Info("A similar product was found. We won't add the product.");
                }
            }
            else
            {
                productDataServices.AddProduct(product);
                log.Info("The new product was added!");
            }
        }

        public void DeleteProduct(Product product)
        {
            productDataServices.DeleteProduct(product);
        }

        public IList<Product> GetListOfProducts()

        {
           return productDataServices.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return productDataServices.GetProductById(id);
        }

        public IList<Product> GetProductsByUserId(int userId)
        {
            return productDataServices.GetProductsByUserId(userId);
        }

        public void UpdateProduct(Product product)
        {
            productDataServices.UpdateProduct(product);        }
    }
}
