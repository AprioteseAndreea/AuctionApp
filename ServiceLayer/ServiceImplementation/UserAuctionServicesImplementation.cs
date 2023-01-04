using DataMapper.SqlServerDAO;
using DataMapper;
using DomainModel;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Utils;

namespace ServiceLayer.ServiceImplementation
{
    public class UserAuctionServicesImplementation : IUserAuctionServices
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductServicesImplementation));
        private readonly IUserAuctionDataServices userAuctionDataServices;
        private readonly IProductDataServices productDataServices;

        public UserAuctionServicesImplementation(IUserAuctionDataServices userAuctionDataServices, IProductDataServices productDataServices)
        {
            this.userAuctionDataServices = userAuctionDataServices;
            this.productDataServices = productDataServices;
        }
        public void AddUserAuction(UserAuction userAuction)
        {
            log.Info("In AddUserAuction method");

            var product = productDataServices.GetProductById(userAuction.Product);

            IList<UserAuction> userAuctions = userAuctionDataServices.GetUserAuctionsByUserIdandProductId(userAuction.User, userAuction.Product);
            if (product.StartingPrice.Currency != userAuction.Price.Currency)
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
            userAuctionDataServices.DeleteUserAuction(userAuction);
        }

        public IList<UserAuction> GetListOfUserAuctions()
        {
            return userAuctionDataServices.GetListOfUserAuctions();
        }

        public UserAuction GetUserAuctionById(int id)
        {
            return (userAuctionDataServices.GetUserAuctionById(id));
        }

        public IList<UserAuction> GetUserAuctionsByUserId(int userId)
        {
            return userAuctionDataServices.GetUserAuctionsByUserId((int)userId);
        }

        public IList<UserAuction> GetUserAuctionsByUserIdandProductId(int userId, int productId)
        {
            return userAuctionDataServices.GetUserAuctionsByUserIdandProductId(userId, productId);
        }

        public void UpdateUserAuction(UserAuction userAuction)
        {
            userAuctionDataServices.UpdateUserAuction(userAuction);
        }
    }
}
