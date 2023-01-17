// <copyright file="SQLServerDAOFactory.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System;

    public class SQLServerDAOFactory : IDAOFactory
    {
        /// <summary>
        /// Gets the user data services.
        /// </summary>
        /// <value>
        /// The user data services.
        /// </value>
        public IUserDataServices UserDataServices
        {
            get
            {
                return new SQLUserDataServices();
            }
        }

        /// <summary>
        /// Gets the produc data services.
        /// </summary>
        /// <value>
        /// The produc data services.
        /// </value>
        public IProductDataServices ProducDataServices
        {
            get
            {
                return new SQLProductDataServices();
            }
        }

        /// <summary>
        /// Gets the user auction data services.
        /// </summary>
        /// <value>
        /// The user auction data services.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public IUserAuctionDataServices UserAuctionDataServices => throw new NotImplementedException();

        /// <summary>
        /// Gets the category data services.
        /// </summary>
        /// <value>
        /// The category data services.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public ICategoryDataServices CategoryDataServices => throw new NotImplementedException();

        /// <summary>
        /// Gets the category relation data services.
        /// </summary>
        /// <value>
        /// The category relation data services.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public ICategoryRelationDataServices CategoryRelationDataServices => throw new NotImplementedException();
    }
}
