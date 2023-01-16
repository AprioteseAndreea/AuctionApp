// <copyright file="ProductServicesImplementation.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace ServiceLayer.ServiceImplementation
{
    using System.Collections.Generic;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using DomainModel.DTO;
    using log4net;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using ServiceLayer.Utils;

    public class ProductServicesImplementation : IProductServices
    {
        private readonly ILog log;
        private readonly IProductDataServices productDataServices;
        private readonly IConfigurationDataServices configurationDataServices;
        private readonly IUserDataServices userDataServices;
        private readonly ICategoryDataServices categoryDataServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductServicesImplementation"/> class.
        /// </summary>
        /// <param name="productDataServices">The product data services.</param>
        /// <param name="configurationDataServices">The configuration data services.</param>
        /// <param name="userDataServices">The user data services.</param>
        /// <param name="categoryDataServices">The category data services.</param>
        /// <param name="log">The log.</param>
        public ProductServicesImplementation(
            IProductDataServices productDataServices,
            IConfigurationDataServices configurationDataServices,
            IUserDataServices userDataServices,
            ICategoryDataServices categoryDataServices,
            ILog log)
        {
            this.productDataServices = productDataServices;
            this.configurationDataServices = configurationDataServices;
            this.userDataServices = userDataServices;
            this.categoryDataServices = categoryDataServices;
            this.log = log;
        }

        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void AddProduct(ProductDTO product)
        {
            this.log.Info("In AddProduct method");

            this.ValidateProduct(product);
            this.CheckMaxAuctionsPerUser(product);
            this.CheckLevenshteinDistance(product);

            this.log.Info("The new product was added!");

            this.productDataServices.AddProduct(this.GetProductFromProductDto(product));
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <exception cref="ServiceLayer.Utils.ObjectNotFoundException"></exception>
        public void DeleteProduct(ProductDTO product)
        {
            this.log.Info("In DeleteProduct method");

            this.ValidateProduct(product);

            var currentProduct = this.productDataServices.GetProductById(product.Id);

            if (currentProduct == null)
            {
                this.log.Warn("The product that you want to delete can not be found!");
                throw new ObjectNotFoundException(product.Name);
            }

            this.log.Info("The product have been deleted!");
            this.productDataServices.DeleteProduct(this.GetProductFromProductDto(product));
        }

        /// <summary>
        /// Gets the list of products.
        /// </summary>
        /// <returns></returns>
        public IList<ProductDTO> GetListOfProducts()
        {
            this.log.Info("In GetListOfProducts method.");
            return this.productDataServices.GetAllProducts().Select(c => new ProductDTO(c)).ToList();
        }

        /// <summary>
        /// Gets the open products by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="ServiceLayer.Utils.IncorrectIdException"></exception>
        /// <exception cref="ServiceLayer.Utils.ObjectNotFoundException"></exception>
        public IList<ProductDTO> GetOpenProductsByUserId(int userId)
        {
            this.log.Info("In GetOpenProductsByUserId method.");

            if (userId < 0 || userId == 0)
            {
                this.log.Warn("The user id is less than 0 or is equal with 0.");
                throw new IncorrectIdException();
            }
            else
            {
                var currentUser = this.userDataServices.GetUserById(userId);
                if (currentUser != null)
                {
                    this.log.Info("The open open products were searched.");
                    return this.productDataServices.GetOpenProductsByUserId(userId).Select(c => new ProductDTO(c)).ToList();
                }
                else
                {
                    this.log.Info("The user whose products you want to select does not exist in the database.");
                    throw new ObjectNotFoundException(userId.ToString());
                }
            }
        }

        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="ServiceLayer.Utils.IncorrectIdException"></exception>
        public ProductDTO GetProductById(int id)
        {
            this.log.Info("In GetProductById method");

            if (id < 0 || id == 0)
            {
                this.log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();
            }

            this.log.Info("The function GetProductById was successfully called.");
            return new ProductDTO(this.productDataServices.GetProductById(id));
        }

        /// <summary>
        /// Gets the products by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="ServiceLayer.Utils.IncorrectIdException"></exception>
        /// <exception cref="ServiceLayer.Utils.ObjectNotFoundException"></exception>
        public IList<ProductDTO> GetProductsByUserId(int userId)
        {
            this.log.Info("In GetProductsByUserId method");

            if (userId < 0 || userId == 0)
            {
                this.log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();
            }
            else
            {
                var currentUser = this.userDataServices.GetUserById(userId);
                if (currentUser == null)
                {
                    this.log.Warn("The ObjectNotFoundException was thrown!");
                    throw new ObjectNotFoundException(userId.ToString());
                }

                this.log.Info("The function GetProductsByUserId was successfully called.");
                return this.productDataServices.GetProductsByUserId(userId).Select(p => new ProductDTO(p)).ToList();
            }
        }

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <exception cref="ServiceLayer.Utils.ObjectNotFoundException"></exception>
        public void UpdateProduct(ProductDTO product)
        {
            this.log.Info("In UpdateProduct method");

            this.ValidateProduct(product);
            var currentProduct = this.productDataServices.GetProductById(product.Id);
            if (currentProduct == null)
            {
                this.log.Warn("The ObjectNotFoundException was thrown!");
                throw new ObjectNotFoundException(product.Name);
            }

            this.log.Info("The function UpdateProduct was successfully called.");
            this.productDataServices.UpdateProduct(this.GetProductFromProductDto(product));
        }

        /// <summary>
        /// Checks the maximum auctions per user.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <exception cref="ServiceLayer.Utils.MaxAuctionsException"></exception>
        private void CheckMaxAuctionsPerUser(ProductDTO product)
        {
            var openAuctions = this.productDataServices.GetOpenProductsByUserId(product.OwnerUserId);
            var maxAuctions = this.configurationDataServices.GetConfigurationById(1);

            if (openAuctions.Count == maxAuctions.MaxAuctions)
            {
                this.log.Warn("The maximum number of licitations has been reached!");
                throw new MaxAuctionsException();
            }
        }

        /// <summary>
        /// Validates the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <exception cref="ServiceLayer.Utils.InvalidObjectException"></exception>
        private void ValidateProduct(ProductDTO product)
        {
            ValidationResults validationResults = Validation.Validate(product);
            if (validationResults.Count != 0)
            {
                throw new InvalidObjectException();
            }
        }

        /// <summary>
        /// Gets the product from product dto.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        private Product GetProductFromProductDto(ProductDTO product)
        {
            var currentUser = this.userDataServices.GetUserById(product.OwnerUserId);
            var currentCategory = this.categoryDataServices.GetCategoryById(product.CategoryId);

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
                Status = product.Status,
            };

            return currentProduct;
        }

        /// <summary>
        /// Checks the levenshtein distance.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <exception cref="ServiceLayer.Utils.SimilarDescriptionException"></exception>
        private void CheckLevenshteinDistance(ProductDTO product)
        {
            var userProducts = this.productDataServices.GetProductsByUserId(product.OwnerUserId);
            if (userProducts.Count != 0)
            {
                this.log.Info("User products have been found.");

                var hasFound = false;
                foreach (var item in userProducts)
                {
                    var distance = StringDistance.LevenshteinDistance(item.Description, product.Description);
                    var currentItem = item;
                    if (distance < 20)
                    {
                        this.log.Info("A similar product has been found. We won't add the product.");

                        hasFound = true;
                        break;
                    }
                }

                if (!hasFound)
                {
                    this.productDataServices.AddProduct(this.GetProductFromProductDto(product));
                    this.log.Info("The new product was added!");
                }
                else
                {
                    this.log.Info("A similar product was found. We won't add the product.");
                    throw new SimilarDescriptionException(product.Name);
                }
            }
        }
    }
}
