// <copyright file="SQLUserAuctionDataServices.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System.Collections.Generic;
    using System.Linq;
    using DomainModel;

    public class SQLUserAuctionDataServices : IUserAuctionDataServices
    {
        /// <summary>
        /// Adds the user auction.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        public void AddUserAuction(UserAuction userAuction)
        {
            using (var context = new MyApplicationContext())
            {
                context.UserAuctions.Add(userAuction);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the user auction.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        public void DeleteUserAuction(UserAuction userAuction)
        {
            using (var context = new MyApplicationContext())
            {
                context.UserAuctions.Attach(userAuction);
                context.UserAuctions.Remove(userAuction);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the list of user auctions.
        /// </summary>
        /// <returns></returns>
        public IList<UserAuction> GetListOfUserAuctions()
        {
            using (var context = new MyApplicationContext())
            {
                return context.UserAuctions.Select(user => user).ToList();
            }
        }

        /// <summary>
        /// Gets the user auction by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public UserAuction GetUserAuctionById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.UserAuctions.Where(user => user.Id == id).SingleOrDefault();
            }
        }

        /// <summary>
        /// Gets the user auctions by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IList<UserAuction> GetUserAuctionsByUserId(int userId)
        {
            using (var context = new MyApplicationContext())
            {
                return context.UserAuctions.Where(user => user.User.Id == userId).ToList();
            }
        }

        /// <summary>
        /// Gets the user auctions by user idand product identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        public IList<UserAuction> GetUserAuctionsByUserIdandProductId(int userId, int productId)
        {
            using (var context = new MyApplicationContext())
            {
                return context.UserAuctions.Where(user => user.User.Id == userId && user.Product.Id == productId).ToList();
            }
        }

        /// <summary>
        /// Updates the user auction.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        public void UpdateUserAuction(UserAuction userAuction)
        {
            using (var context = new MyApplicationContext())
            {
                var result = context.UserAuctions.First(u => u.Id == userAuction.Id);
                if (result != null)
                {
                    result = userAuction;
                    context.SaveChanges();
                }
            }
        }
    }
}