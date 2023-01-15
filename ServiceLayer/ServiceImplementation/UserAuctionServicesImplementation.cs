using DataMapper.SqlServerDAO;
using DataMapper;
using DomainModel;
using log4net;
using System;
using System.Collections.Generic;
using ServiceLayer.Utils;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using DomainModel.enums;
using DomainModel.DTO;
using System.Linq;

namespace ServiceLayer.ServiceImplementation
{
    public class UserAuctionServicesImplementation : IUserAuctionServices
    {
        private readonly ILog log;
        private readonly IUserAuctionDataServices userAuctionDataServices;
        private readonly IProductDataServices productDataServices;
        private readonly IUserDataServices userDataServices;

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
        public void AddUserAuction(UserAuctionDTO userAuction)
        {
            log.Info("In AddUserAuction method");
           

            var product = productDataServices.GetProductById(userAuction.ProductId);
            if (product == null) throw new NullReferenceException("");

            ValidateUserAuction(userAuction);
            var userAuctions = userAuctionDataServices.GetUserAuctionsByUserIdandProductId(userAuction.UserId, userAuction.ProductId);

            CheckProductStatus(product);
            CheckAuctionCurrency(product, userAuction);
            CheckFirstAuction(userAuction, userAuctions, product);
            CheckAmountRangeForAuction(userAuction, userAuctions);

            log.Info("A new auction was successfully added!");
            userAuctionDataServices.AddUserAuction(GetUserAuctionFromUserAuctionDto(userAuction));
        }
        public void CheckAmountRangeForAuction(UserAuctionDTO userAuction, IList<UserAuction> userAuctions)
        {
            if (userAuctions.Count != 0 && (userAuction.Price.Amount > 3 * userAuctions[userAuctions.Count - 1].Price.Amount || userAuction.Price.Amount <= userAuctions[userAuctions.Count - 1].Price.Amount))
            {
                log.Warn("The amount of the offer is too small or equal to the value of the product or more than 300% of the previous offer.");
                throw new OverbiddingException("");

            }
        }

        public void CheckFirstAuction(UserAuctionDTO userAuction, IList<UserAuction> userAuctions, Product product)
        {
            if (userAuctions.Count == 0 && userAuction.Price.Amount <= product.StartingPrice.Amount)
            {
                log.Warn("The amount of the offer is too small or equal to the value of the product.");
                throw new MinimumBidException(product.Name);

            }

        }
        public void CheckAuctionCurrency(Product product, UserAuctionDTO userAuction)
        {
            if (product.StartingPrice.Currency != userAuction.Price.Currency)
            {
                log.Warn("The currency of the product and the currency of the offer are incompatible.");
                throw new IncompatibleCurrencyException(product.Name);
            }
        }
        public void CheckProductStatus(Product product)
        {
            if (product.Status == AuctionStatus.Closed)
            {
                throw new ClosedAuctionException(product.Name);
            }

        }
        private UserAuction GetUserAuctionFromUserAuctionDto(UserAuctionDTO userAuction)
        {
            var currentUser = userDataServices.GetUserById(userAuction.UserId);
            var product = productDataServices.GetProductById(userAuction.ProductId);

            UserAuction currentUserAuction = new UserAuction
            {
                Id = userAuction.Id,
                Product = product,
                User = currentUser,
                Price = userAuction.Price,
            };

            return currentUserAuction;
        }

        public void DeleteUserAuction(UserAuctionDTO userAuction)
        {
            log.Info("In DeleteUserAuction method");

            ValidateUserAuction(userAuction);

            var currentUserAuction = userAuctionDataServices.GetUserAuctionById(userAuction.Id);
            if (currentUserAuction == null)
            {
                log.Warn("The user auction that you want to delete can not be found!");
                throw new ObjectNotFoundException(userAuction.Id.ToString());
            }

            log.Info("The user auction have been deleted!");
            userAuctionDataServices.DeleteUserAuction(GetUserAuctionFromUserAuctionDto(userAuction));

        }

        public IList<UserAuctionDTO> GetListOfUserAuctions()
        {
            log.Info("In GetListOfUserAuctions method.");
            return userAuctionDataServices.GetListOfUserAuctions().Select(ua => new UserAuctionDTO(ua)).ToList();
        }

        public UserAuctionDTO GetUserAuctionById(int id)
        {
            log.Info("In GetUserAuctionById method.");

            if (id < 0 || id == 0)
            {
                log.Warn("The id is less than 0 or is equal with 0.");
                throw new IncorrectIdException();

            }

            log.Info("The function GetUserAuctionById was successfully called.");
            return new UserAuctionDTO(userAuctionDataServices.GetUserAuctionById(id));
        }

        public IList<UserAuctionDTO> GetUserAuctionsByUserId(int userId)
        {
            log.Info("In GetUserAuctionsByUserId method");

            if (userId < 0 || userId == 0)
            {
                log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();

            }

            var currentUser = userDataServices.GetUserById(userId);
            if (currentUser == null)
            {
                log.Warn("The ObjectNotFoundException was thrown!");
                throw new ObjectNotFoundException(userId.ToString());
            }

            log.Info("The function GetProductsByUserId was successfully called.");
            return userAuctionDataServices.GetUserAuctionsByUserId(userId).Select(ua => new UserAuctionDTO(ua)).ToList();
        }
        public IList<UserAuctionDTO> GetUserAuctionsByUserIdandProductId(int userId, int productId)
        {

            log.Info("In GetUserAuctionsByUserIdandProductId method.");

            if (userId < 0 || userId == 0 || productId < 0 || productId == 0)
            {
                log.Warn("The userId or productId is less than 0 or is equal with 0.");
                throw new IncorrectIdException();

            }

            log.Info("The function GetUserAuctionById was successfully called.");
            return userAuctionDataServices.GetUserAuctionsByUserIdandProductId(userId, productId).Select(ua => new UserAuctionDTO(ua)).ToList();

        }

        public void UpdateUserAuction(UserAuctionDTO userAuction)
        {
            log.Info("In UpdateProduct method");

            ValidateUserAuction(userAuction);

            var currentUserAuction = userAuctionDataServices.GetUserAuctionById(userAuction.Id);
            if (currentUserAuction == null)
            {
                log.Warn("The NullReferenceException was thrown!");
                throw new NullReferenceException("The object can not be null.");
            }

            log.Info("The function UpdateUserAuction was successfully called.");
            userAuctionDataServices.UpdateUserAuction(GetUserAuctionFromUserAuctionDto(userAuction));

        }
        private void ValidateUserAuction(UserAuctionDTO userAuction)
        {
            ValidationResults validationResults = Validation.Validate(userAuction);
            if (validationResults.Count != 0) throw new InvalidObjectException();
        }
    }
}
