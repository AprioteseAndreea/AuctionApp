// <copyright file="IUserAuctionDataServices.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DataMapper
{
    using System.Collections.Generic;
    using DomainModel;

    public interface IUserAuctionDataServices
    {
        /// <summary>
        /// Gets the list of user auctions.
        /// </summary>
        /// <returns></returns>
        IList<UserAuction> GetListOfUserAuctions();

        /// <summary>
        /// Deletes the user auction.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        void DeleteUserAuction(UserAuction userAuction);

        /// <summary>
        /// Updates the user auction.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        void UpdateUserAuction(UserAuction userAuction);

        /// <summary>
        /// Gets the user auction by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        UserAuction GetUserAuctionById(int id);

        /// <summary>
        /// Gets the user auctions by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IList<UserAuction> GetUserAuctionsByUserId(int userId);

        /// <summary>
        /// Adds the user auction.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        void AddUserAuction(UserAuction userAuction);

        /// <summary>
        /// Gets the user auctions by user idand product identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        IList<UserAuction> GetUserAuctionsByUserIdandProductId(int userId, int productId);
    }
}
