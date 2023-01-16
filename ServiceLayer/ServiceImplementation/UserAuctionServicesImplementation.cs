namespace ServiceLayer.ServiceImplementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using DomainModel.DTO;
    using DomainModel.Enums;
    using log4net;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using ServiceLayer.Utils;

    public class UserAuctionServicesImplementation : IUserAuctionServices
    {
        private readonly ILog log;
        private readonly IUserAuctionDataServices userAuctionDataServices;
        private readonly IProductDataServices productDataServices;
        private readonly IUserDataServices userDataServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuctionServicesImplementation"/> class.
        /// </summary>
        /// <param name="userAuctionDataServices">The user auction data services.</param>
        /// <param name="productDataServices">The product data services.</param>
        /// <param name="userDataServices">The user data services.</param>
        /// <param name="log">The log.</param>
        public UserAuctionServicesImplementation(
            IUserAuctionDataServices userAuctionDataServices,
            IProductDataServices productDataServices,
            IUserDataServices userDataServices,
            ILog log)
        {
            this.userAuctionDataServices = userAuctionDataServices;
            this.productDataServices = productDataServices;
            this.userDataServices = userDataServices;
            this.log = log;
        }

        /// <summary>
        /// Adds the user auction.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        /// <exception cref="System.NullReferenceException"></exception>
        public void AddUserAuction(UserAuctionDTO userAuction)
        {
            this.log.Info("In AddUserAuction method");

            var product = this.productDataServices.GetProductById(userAuction.ProductId);
            if (product == null)
            {
                throw new NullReferenceException(string.Empty);
            }

            this.ValidateUserAuction(userAuction);
            var userAuctions = this.userAuctionDataServices.GetUserAuctionsByUserIdandProductId(userAuction.UserId, userAuction.ProductId);

            this.CheckProductStatus(product);
            this.CheckAuctionCurrency(product, userAuction);
            this.CheckFirstAuction(userAuction, userAuctions, product);
            this.CheckAmountRangeForAuction(userAuction, userAuctions);

            this.log.Info("A new auction was successfully added!");
            this.userAuctionDataServices.AddUserAuction(this.GetUserAuctionFromUserAuctionDto(userAuction));
        }

        /// <summary>
        /// Checks the amount range for auction.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        /// <param name="userAuctions">The user auctions.</param>
        /// <exception cref="ServiceLayer.Utils.OverbiddingException"></exception>
        public void CheckAmountRangeForAuction(UserAuctionDTO userAuction, IList<UserAuction> userAuctions)
        {
            if (userAuctions.Count != 0 && (userAuction.Price.Amount > 3 * userAuctions[userAuctions.Count - 1].Price.Amount || userAuction.Price.Amount <= userAuctions[userAuctions.Count - 1].Price.Amount))
            {
                this.log.Warn("The amount of the offer is too small or equal to the value of the product or more than 300% of the previous offer.");
                throw new OverbiddingException(string.Empty);
            }
        }

        /// <summary>
        /// Checks the first auction.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        /// <param name="userAuctions">The user auctions.</param>
        /// <param name="product">The product.</param>
        /// <exception cref="ServiceLayer.Utils.MinimumBidException"></exception>
        public void CheckFirstAuction(UserAuctionDTO userAuction, IList<UserAuction> userAuctions, Product product)
        {
            if (userAuctions.Count == 0 && userAuction.Price.Amount <= product.StartingPrice.Amount)
            {
                this.log.Warn("The amount of the offer is too small or equal to the value of the product.");
                throw new MinimumBidException(product.Name);
            }
        }

        /// <summary>
        /// Checks the auction currency.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="userAuction">The user auction.</param>
        /// <exception cref="ServiceLayer.Utils.IncompatibleCurrencyException"></exception>
        public void CheckAuctionCurrency(Product product, UserAuctionDTO userAuction)
        {
            if (product.StartingPrice.Currency != userAuction.Price.Currency)
            {
                this.log.Warn("The currency of the product and the currency of the offer are incompatible.");
                throw new IncompatibleCurrencyException(product.Name);
            }
        }

        /// <summary>
        /// Checks the product status.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <exception cref="ServiceLayer.Utils.ClosedAuctionException"></exception>
        public void CheckProductStatus(Product product)
        {
            if (product.Status == AuctionStatus.Closed)
            {
                throw new ClosedAuctionException(product.Name);
            }
        }

        /// <summary>
        /// Deletes the user auction.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        /// <exception cref="ServiceLayer.Utils.ObjectNotFoundException"></exception>
        public void DeleteUserAuction(UserAuctionDTO userAuction)
        {
            this.log.Info("In DeleteUserAuction method");

            this.ValidateUserAuction(userAuction);

            var currentUserAuction = this.userAuctionDataServices.GetUserAuctionById(userAuction.Id);
            if (currentUserAuction == null)
            {
                this.log.Warn("The user auction that you want to delete can not be found!");
                throw new ObjectNotFoundException(userAuction.Id.ToString());
            }

            this.log.Info("The user auction have been deleted!");
            this.userAuctionDataServices.DeleteUserAuction(this.GetUserAuctionFromUserAuctionDto(userAuction));
        }

        /// <summary>
        /// Gets the list of user auctions.
        /// </summary>
        /// <returns></returns>
        public IList<UserAuctionDTO> GetListOfUserAuctions()
        {
            this.log.Info("In GetListOfUserAuctions method.");
            return this.userAuctionDataServices.GetListOfUserAuctions().Select(ua => new UserAuctionDTO(ua)).ToList();
        }

        /// <summary>
        /// Gets the user auction by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="ServiceLayer.Utils.IncorrectIdException"></exception>
        public UserAuctionDTO GetUserAuctionById(int id)
        {
            this.log.Info("In GetUserAuctionById method.");

            if (id < 0 || id == 0)
            {
                this.log.Warn("The id is less than 0 or is equal with 0.");
                throw new IncorrectIdException();
            }

            this.log.Info("The function GetUserAuctionById was successfully called.");
            return new UserAuctionDTO(this.userAuctionDataServices.GetUserAuctionById(id));
        }

        /// <summary>
        /// Gets the user auctions by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="ServiceLayer.Utils.IncorrectIdException"></exception>
        /// <exception cref="ServiceLayer.Utils.ObjectNotFoundException"></exception>
        public IList<UserAuctionDTO> GetUserAuctionsByUserId(int userId)
        {
            this.log.Info("In GetUserAuctionsByUserId method");

            if (userId < 0 || userId == 0)
            {
                this.log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();
            }

            var currentUser = this.userDataServices.GetUserById(userId);
            if (currentUser == null)
            {
                this.log.Warn("The ObjectNotFoundException was thrown!");
                throw new ObjectNotFoundException(userId.ToString());
            }

            this.log.Info("The function GetProductsByUserId was successfully called.");
            return this.userAuctionDataServices.GetUserAuctionsByUserId(userId).Select(ua => new UserAuctionDTO(ua)).ToList();
        }

        /// <summary>
        /// Gets the user auctions by user idand product identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        /// <exception cref="ServiceLayer.Utils.IncorrectIdException"></exception>
        public IList<UserAuctionDTO> GetUserAuctionsByUserIdandProductId(int userId, int productId)
        {
            this.log.Info("In GetUserAuctionsByUserIdandProductId method.");

            if (userId < 0 || userId == 0 || productId < 0 || productId == 0)
            {
                this.log.Warn("The userId or productId is less than 0 or is equal with 0.");
                throw new IncorrectIdException();
            }

            this.log.Info("The function GetUserAuctionById was successfully called.");
            return this.userAuctionDataServices.GetUserAuctionsByUserIdandProductId(userId, productId).Select(ua => new UserAuctionDTO(ua)).ToList();
        }

        /// <summary>
        /// Updates the user auction.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        /// <exception cref="System.NullReferenceException">The object can not be null.</exception>
        public void UpdateUserAuction(UserAuctionDTO userAuction)
        {
            this.log.Info("In UpdateProduct method");

            this.ValidateUserAuction(userAuction);

            var currentUserAuction = this.userAuctionDataServices.GetUserAuctionById(userAuction.Id);
            if (currentUserAuction == null)
            {
                this.log.Warn("The NullReferenceException was thrown!");
                throw new NullReferenceException("The object can not be null.");
            }

            this.log.Info("The function UpdateUserAuction was successfully called.");
            this.userAuctionDataServices.UpdateUserAuction(this.GetUserAuctionFromUserAuctionDto(userAuction));
        }

        /// <summary>
        /// Validates the user auction.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        /// <exception cref="ServiceLayer.Utils.InvalidObjectException"></exception>
        private void ValidateUserAuction(UserAuctionDTO userAuction)
        {
            ValidationResults validationResults = Validation.Validate(userAuction);
            if (validationResults.Count != 0)
            {
                throw new InvalidObjectException();
            }
        }

        /// <summary>
        /// Gets the user auction from user auction dto.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        /// <returns></returns>
        private UserAuction GetUserAuctionFromUserAuctionDto(UserAuctionDTO userAuction)
        {
            var currentUser = this.userDataServices.GetUserById(userAuction.UserId);
            var product = this.productDataServices.GetProductById(userAuction.ProductId);

            UserAuction currentUserAuction = new UserAuction
            {
                Id = userAuction.Id,
                Product = product,
                User = currentUser,
                Price = userAuction.Price,
            };

            return currentUserAuction;
        }
    }
}
