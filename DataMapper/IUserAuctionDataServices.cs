using DomainModel;
using System.Collections.Generic;

namespace DataMapper
{
   public interface IUserAuctionDataServices
    {
        IList<UserAuction> GetListOfUserAuctions();
        void DeleteUserAuction(UserAuction userAuction);
        void UpdateUserAuction(UserAuction userAuction);
        UserAuction GetUserAuctionById(int id);
        IList<UserAuction> GetUserAuctionsByUserId(int userId);
        void AddUserAuction(UserAuction userAuction);
        IList<UserAuction> GetUserAuctionsByUserIdandProductId(int userId, int productId);
    }
}
