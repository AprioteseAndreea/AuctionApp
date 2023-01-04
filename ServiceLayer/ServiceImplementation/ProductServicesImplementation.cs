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
using System.Runtime.InteropServices.ComTypes;

namespace ServiceLayer.ServiceImplementation
{
    public class ProductServicesImplementation : IProductServices
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductServicesImplementation));
        private readonly IProductDataServices productDataServices;
        private readonly IConfigurationDataServices configurationDataServices;

        public ProductServicesImplementation(IProductDataServices productDataServices, IConfigurationDataServices configurationDataServices)
        {
            this.productDataServices = productDataServices;
            this.configurationDataServices = configurationDataServices;
        }
        public static IProductDataServices GetProductDataServices()
        {
            return new SQLProductDataServices();
        }

        public static IConfigurationDataServices GetConfigurationDataServices()
        {
            return new SQLConfigurationDataServices();
        }

        public void AddProduct(Product product)
        {
            log.Info("In AddProduct method");
            var userProducts = productDataServices.GetProductsByUserId(product.OwnerUser.Id);
            IList<Product> openAuctions = productDataServices.GetOpenProductsByUserId(product.OwnerUser.Id);
            var maxAuctions = configurationDataServices.GetConfigurationById(1);

            if (openAuctions.Count == maxAuctions.MaxAuctions)
            {
                log.Warn("The maximum number of licitations has been reached!");
                throw new MaxAuctionsException();
            }
            if (userProducts != null)
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
                    log.Info("A similar product was found. We won't add the product.");
                    throw new SimilarDescriptionException(product.Name);
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

        public IList<Product> GetOpenProductsByUserId(int userId)
        {
            throw new NotImplementedException();
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
