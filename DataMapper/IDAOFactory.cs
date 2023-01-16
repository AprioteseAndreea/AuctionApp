// <copyright file="IDAOFactory.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DataMapper
{
    public interface IDAOFactory
    {
        /// <summary>
        /// Gets the user data services.
        /// </summary>
        /// <value>
        /// The user data services.
        /// </value>
        IUserDataServices UserDataServices { get; }

        /// <summary>
        /// Gets the produc data services.
        /// </summary>
        /// <value>
        /// The produc data services.
        /// </value>
        IProductDataServices ProducDataServices { get; }

        /// <summary>
        /// Gets the user auction data services.
        /// </summary>
        /// <value>
        /// The user auction data services.
        /// </value>
        IUserAuctionDataServices UserAuctionDataServices { get; }

        /// <summary>
        /// Gets the category data services.
        /// </summary>
        /// <value>
        /// The category data services.
        /// </value>
        ICategoryDataServices CategoryDataServices { get; }

        /// <summary>
        /// Gets the category relation data services.
        /// </summary>
        /// <value>
        /// The category relation data services.
        /// </value>
        ICategoryRelationDataServices CategoryRelationDataServices { get; }
    }
}
