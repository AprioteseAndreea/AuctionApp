using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
   public interface IUserAuctionServices
    {
        IList<UserAuction> GetListOfUserAuctions();

        void DeleteUserAuction(UserAuction userAuction);

        void UpdateUserAuction(UserAuction userAuction);

        UserAuction GetUserAuctionById(int id);
        IList<UserAuction> GetUserAuctionsByUserId(int userId);
        IList<UserAuction> GetUserAuctionsByUserIdandProductId(int userId, int productId);


        void AddUserAuction(UserAuction userAuction);
    }
}
