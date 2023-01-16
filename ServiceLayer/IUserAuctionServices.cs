namespace ServiceLayer
{
    using System.Collections.Generic;
    using DomainModel.DTO;

    public interface IUserAuctionServices
    {
        /// <summary>
        /// Gets the list of user auctions.
        /// </summary>
        /// <returns></returns>
        IList<UserAuctionDTO> GetListOfUserAuctions();

        /// <summary>
        /// Deletes the user auction.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        void DeleteUserAuction(UserAuctionDTO userAuction);

        /// <summary>
        /// Updates the user auction.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        void UpdateUserAuction(UserAuctionDTO userAuction);

        /// <summary>
        /// Gets the user auction by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        UserAuctionDTO GetUserAuctionById(int id);

        /// <summary>
        /// Gets the user auctions by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IList<UserAuctionDTO> GetUserAuctionsByUserId(int userId);

        /// <summary>
        /// Gets the user auctions by user idand product identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        IList<UserAuctionDTO> GetUserAuctionsByUserIdandProductId(int userId, int productId);

        /// <summary>
        /// Adds the user auction.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        void AddUserAuction(UserAuctionDTO userAuction);
    }
}
