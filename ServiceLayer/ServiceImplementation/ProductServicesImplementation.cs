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
using DomainModel.DTO;

namespace ServiceLayer.ServiceImplementation
{
    public class ProductServicesImplementation : IProductServices
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductServicesImplementation));
        private readonly IProductDataServices productDataServices;
        private readonly IConfigurationDataServices configurationDataServices;
        private readonly IUserDataServices userDataServices;
        private readonly ICategoryDataServices categoryDataServices;


        public ProductServicesImplementation(IProductDataServices productDataServices, IConfigurationDataServices configurationDataServices, IUserDataServices userDataServices, ICategoryDataServices categoryDataServices)
        {
            this.productDataServices = productDataServices;
            this.configurationDataServices = configurationDataServices;
            this.userDataServices = userDataServices;
            this.categoryDataServices = categoryDataServices;
        }

        public void AddProduct(ProductDTO product)
        {
            log.Info("In AddProduct method");

            ValidateProduct(product);
            CheckMaxAuctionsPerUser(product);
            CheckLevenshteinDistance(product);

            log.Info("The new product was added!");

            productDataServices.AddProduct(GetProductFromProductDto(product));

        }
        private Product GetProductFromProductDto(ProductDTO product)
        {
            var currentUser = userDataServices.GetUserById(product.OwnerUserId);
            var currentCategory = categoryDataServices.GetCategoryById(product.CategoryId);
           
            Product currentProduct = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                OwnerUser = currentUser,
                StartDate = product.StartDate,
                EndDate = product.EndDate,
                StartingPrice = product.StartingPrice,
                Category = currentCategory,
                Status = product.Status
            };

            return currentProduct;
        }
        private void CheckLevenshteinDistance(ProductDTO product)
        {
            var userProducts = productDataServices.GetProductsByUserId(product.OwnerUserId);
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
                    productDataServices.AddProduct(GetProductFromProductDto(product));
                    log.Info("The new product was added!");
                }
                else
                {
                    log.Info("A similar product was found. We won't add the product.");
                    throw new SimilarDescriptionException(product.Name);
                }
            }

        }
        private void CheckMaxAuctionsPerUser(ProductDTO product)
        {
            var openAuctions = productDataServices.GetOpenProductsByUserId(product.OwnerUserId);
            var maxAuctions = configurationDataServices.GetConfigurationById(1);

            if (openAuctions.Count == maxAuctions.MaxAuctions)
            {
                log.Warn("The maximum number of licitations has been reached!");
                throw new MaxAuctionsException();
            }
        }
        private void ValidateProduct(ProductDTO product)
        {
            ValidationResults validationResults = Validation.Validate(product);
            if (validationResults.Count != 0) throw new InvalidObjectException();
        }

        public void DeleteProduct(ProductDTO product)
        {
            log.Info("In DeleteProduct method");

            ValidateProduct(product);

            var currentProduct = productDataServices.GetProductById(product.Id);

            if (currentProduct == null)
            {
                log.Warn("The product that you want to delete can not be found!");
                throw new ObjectNotFoundException(product.Name);

            }

            log.Info("The product have been deleted!");
            productDataServices.DeleteProduct(GetProductFromProductDto(product));

        }

        public IList<ProductDTO> GetListOfProducts()
        {
            log.Info("In GetListOfProducts method.");
            return productDataServices.GetAllProducts().Select(c => new ProductDTO(c)).ToList();
        }

        public IList<ProductDTO> GetOpenProductsByUserId(int userId)
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
                    return productDataServices.GetOpenProductsByUserId(userId).Select(c => new ProductDTO(c)).ToList();

                }
                else
                {
                    log.Info("The user whose products you want to select does not exist in the database.");
                    throw new ObjectNotFoundException(userId.ToString());

                }

            }
        }

        public ProductDTO GetProductById(int id)
        {
            log.Info("In GetProductById method");

            if (id < 0 || id == 0)
            {
                log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();

            }

            log.Info("The function GetProductById was successfully called.");
            return new ProductDTO(productDataServices.GetProductById(id));

        }

        public IList<ProductDTO> GetProductsByUserId(int userId)
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
                if (currentUser == null)
                {

                    log.Warn("The ObjectNotFoundException was thrown!");
                    throw new ObjectNotFoundException(userId.ToString());

                }

                log.Info("The function GetProductsByUserId was successfully called.");
                return productDataServices.GetProductsByUserId(userId).Select(p => new ProductDTO(p)).ToList();

            }
        }

        public void UpdateProduct(ProductDTO product)
        {
            log.Info("In UpdateProduct method");

            ValidateProduct(product);
            var currentProduct = productDataServices.GetProductById(product.Id);
            if (currentProduct == null)
            {
                log.Warn("The ObjectNotFoundException was thrown!");
                throw new ObjectNotFoundException(product.Name);

            }
            log.Info("The function UpdateProduct was successfully called.");
            productDataServices.UpdateProduct(GetProductFromProductDto(product));
        }
    }
}
