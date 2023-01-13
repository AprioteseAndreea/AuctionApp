using DataMapper.SqlServerDAO;
using DataMapper;
using DomainModel;
using log4net;
using System;
using System.Collections.Generic;
using ServiceLayer.Utils;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace ServiceLayer.ServiceImplementation
{
    public class UserAuctionServicesImplementation : IUserAuctionServices
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductServicesImplementation));
        private readonly IUserAuctionDataServices userAuctionDataServices;
        private readonly IProductDataServices productDataServices;
        private readonly IUserDataServices userDataServices;

        public UserAuctionServicesImplementation(IUserAuctionDataServices userAuctionDataServices, IProductDataServices productDataServices, IUserDataServices userDataServices)
        {
            this.userAuctionDataServices = userAuctionDataServices;
            this.productDataServices = productDataServices;
            this.userDataServices = userDataServices;

        }
        public void AddUserAuction(UserAuction userAuction)
        {
            log.Info("In AddUserAuction method");

            var product = productDataServices.GetProductById(userAuction.Product.Id);
            IList<UserAuction> userAuctions = new List<UserAuction>();

            if (userAuction.Product!=null && userAuction.User != null)
            {
               userAuctions = userAuctionDataServices.GetUserAuctionsByUserIdandProductId(userAuction.User.Id, userAuction.Product.Id);
            }
  
            ValidationResults validationResults = Validation.Validate(userAuction);
            if(validationResults.Count != 0)
            {
                throw new InvalidObjectException();
            }
            else if (product.Status == "Closed")
            {
                throw new ClosedAuctionException(product.Name);
            }
            else if (product.StartingPrice.Currency != userAuction.Price.Currency)
            {
                log.Warn("The currency of the product and the currency of the offer are incompatible.");
                throw new IncompatibleCurrencyException(product.Name);
            }
            else if (userAuctions.Count == 0 && userAuction.Price.Amount <= product.StartingPrice.Amount)
            {
                log.Warn("The amount of the offer is too small or equal to the value of the product.");
                throw new MinimumBidException(product.Name);

            }
            else if (userAuctions.Count != 0 && (userAuction.Price.Amount > 3 * userAuctions[userAuctions.Count - 1].Price.Amount || userAuction.Price.Amount <= userAuctions[userAuctions.Count - 1].Price.Amount))
            {
                log.Warn("The amount of the offer is too small or equal to the value of the product or more than 300% of the previous offer.");
                throw new OverbiddingException(product.Name);

            }
            else
            {
                userAuctionDataServices.AddUserAuction(userAuction);
                log.Info("A new auction was successfully added!");
            }

        }

        public void DeleteUserAuction(UserAuction userAuction)
        {
            log.Info("In DeleteUserAuction method");

            if (userAuction != null)
            {
                var currentUserAuction = userAuctionDataServices.GetUserAuctionById(userAuction.Id);
                if (currentUserAuction != null)
                {
                    log.Info("The user auction have been deleted!");
                    userAuctionDataServices.DeleteUserAuction(userAuction);
                }
                else
                {
                    log.Warn("The user auction that you want to delete can not be found!");
                    throw new ObjectNotFoundException(userAuction.Id.ToString());
                }
            }
            else
            {
                log.Warn("The object passed by parameter is null.");
                throw new NullReferenceException("The object can not be null.");
            }
        }

        public IList<UserAuction> GetListOfUserAuctions()
        {
            log.Info("In GetListOfUserAuctions method.");
            return userAuctionDataServices.GetListOfUserAuctions();
        }

        public UserAuction GetUserAuctionById(int id)
        {
            log.Info("In GetUserAuctionById method.");

            if (id < 0 || id == 0)
            {
                log.Warn("The id is less than 0 or is equal with 0.");
                throw new IncorrectIdException();

            }
            else
            {
                log.Info("The function GetUserAuctionById was successfully called.");
                return userAuctionDataServices.GetUserAuctionById(id);


            }
        }

        public IList<UserAuction> GetUserAuctionsByUserId(int userId)
        {
            log.Info("In GetUserAuctionsByUserId method");

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
                    return userAuctionDataServices.GetUserAuctionsByUserId((int)userId);

                }
                else
                {
                    log.Warn("The ObjectNotFoundException was thrown!");
                    throw new ObjectNotFoundException(userId.ToString());

                }

            }
        }

        public IList<UserAuction> GetUserAuctionsByUserIdandProductId(int userId, int productId)
        {
          
            log.Info("In GetUserAuctionsByUserIdandProductId method.");

            if (userId < 0 || userId == 0 || productId < 0 || productId == 0)
            {
                log.Warn("The userId or productId is less than 0 or is equal with 0.");
                throw new IncorrectIdException();

            }
            else
            {
                log.Info("The function GetUserAuctionById was successfully called.");
                return userAuctionDataServices.GetUserAuctionsByUserIdandProductId(userId, productId);

            }
        }

        public void UpdateUserAuction(UserAuction userAuction)
        {
            userAuctionDataServices.UpdateUserAuction(userAuction);
            log.Info("In UpdateProduct method");

            if (userAuction != null)
            {
                var currentUserAuction = userAuctionDataServices.GetUserAuctionById(userAuction.Id);
                if (currentUserAuction != null)
                {
                    log.Info("The function UpdateUserAuction was successfully called.");
                    userAuctionDataServices.UpdateUserAuction(userAuction);

                }
                else
                {
                    log.Warn("The ObjectNotFoundException was thrown!");
                    throw new ObjectNotFoundException(userAuction.Id.ToString());
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
