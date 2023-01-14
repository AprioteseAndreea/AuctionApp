using DomainModel;
using DomainModel.DTO;
using System;
using System.Collections.Generic;

namespace ServiceLayer
{
    public interface IUserAuctionServices
    {
        IList<UserAuctionDTO> GetListOfUserAuctions();
        void DeleteUserAuction(UserAuctionDTO userAuction);
        void UpdateUserAuction(UserAuctionDTO userAuction);
        UserAuctionDTO GetUserAuctionById(int id);
        IList<UserAuctionDTO> GetUserAuctionsByUserId(int userId);
        IList<UserAuctionDTO> GetUserAuctionsByUserIdandProductId(int userId, int productId);
        void AddUserAuction(UserAuctionDTO userAuction);
    }
}
