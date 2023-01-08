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
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace ServiceLayer.ServiceImplementation
{
    public class ProductServicesImplementation : IProductServices
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductServicesImplementation));
        private readonly IProductDataServices productDataServices;
        private readonly IConfigurationDataServices configurationDataServices;
        private readonly IUserDataServices userDataServices;

        public ProductServicesImplementation(IProductDataServices productDataServices, IConfigurationDataServices configurationDataServices, IUserDataServices userDataServices)
        {
            this.productDataServices = productDataServices;
            this.configurationDataServices = configurationDataServices;
            this.userDataServices = userDataServices;
        }

        public void AddProduct(Product product)
        {
            log.Info("In AddProduct method");

            var userProducts = productDataServices.GetProductsByUserId(product.OwnerUser.Id);
            IList<Product> openAuctions = productDataServices.GetOpenProductsByUserId(product.OwnerUser.Id);
            var maxAuctions = configurationDataServices.GetConfigurationById(1);
            ValidationResults validationResults = Validation.Validate(product);

            if (product == null) throw new NullReferenceException("The object can not be null.");
            else if (validationResults.Count != 0) throw new InvalidObjectException();
            else if (openAuctions.Count == maxAuctions.MaxAuctions)
            {
                log.Warn("The maximum number of licitations has been reached!");
                throw new MaxAuctionsException();
            }
            if (userProducts.Count != 0)
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
            log.Info("In DeleteProduct method");

            if (product != null)
            {
                var currentProduct = productDataServices.GetProductById(product.Id);
                if (currentProduct != null)
                {
                    log.Info("The product have been deleted!");
                    productDataServices.DeleteProduct(product);
                }
                else
                {
                    log.Warn("The product that you want to delete can not be found!");
                    throw new ObjectNotFoundException(product.Name);
                }
            }
            else
            {
                log.Warn("The object passed by parameter is null.");
                throw new NullReferenceException("The object can not be null.");
            }
        }

        public IList<Product> GetListOfProducts()
        {
            log.Info("In GetListOfProducts method.");
            return productDataServices.GetAllProducts();
        }

        public IList<Product> GetOpenProductsByUserId(int userId)
        {
            log.Info("In GetOpenProductsByUserId method.");

            if (userId < 0 || userId == 0)
            {
                log.Warn("The user id is less than 0 or is equal with 0.");
                throw new IncorrectIdException();

            }
            else
            {
                var currentUser = userDataServices.GetUserById(userId);
                if (currentUser != null)
                {
                    log.Info("The open open products were searched.");
                    return productDataServices.GetOpenProductsByUserId(userId);

                }
                else
                {
                    log.Info("The user whose products you want to select does not exist in the database.");
                    throw new ObjectNotFoundException(userId.ToString());

                }

            }
        }

        public Product GetProductById(int id)
        {
            log.Info("In GetProductById method");

            if (id < 0 || id == 0)
            {
                log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();

            }
            else
            {
                log.Info("The function GetProductById was successfully called.");
                return productDataServices.GetProductById(id);

            }

        }

        public IList<Product> GetProductsByUserId(int userId)
        {
            log.Info("In GetProductsByUserId method");

            if (userId < 0 || userId == 0)
            {
                log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();

            }
            else
            {
                var currentUser = userDataServices.GetUserById(userId);
                if (currentUser != null)
                {
                    log.Info("The function GetProductsByUserId was successfully called.");
                    return productDataServices.GetProductsByUserId(userId);

                }
                else
                {
                    log.Warn("The ObjectNotFoundException was thrown!");
                    throw new ObjectNotFoundException(userId.ToString());

                }

            }
        }

        public void UpdateProduct(Product product)
        {
            log.Info("In UpdateProduct method");

            if (product != null)
            {
                var currentProduct = productDataServices.GetProductById(product.Id);
                if (currentProduct != null)
                {
                    log.Info("The function UpdateProduct was successfully called.");
                    productDataServices.UpdateProduct(product);

                }
                else
                {
                    log.Warn("The ObjectNotFoundException was thrown!");
                    throw new ObjectNotFoundException(product.Name);
                }
            }
            else
            {
                log.Warn("The NullReferenceException was thrown!");
                throw new NullReferenceException("The object can not be null.");
            }

        }
    }
}
