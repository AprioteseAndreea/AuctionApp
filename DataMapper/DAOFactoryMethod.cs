﻿// <copyright file="DAOFactoryMethod.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DataMapper
{
    using System.Configuration;
    using DataMapper.SqlServerDAO;

    public static class DAOFactoryMethod
    {
        private static readonly IDAOFactory CurrentDAOFactoryValue;

        /// <summary>
        /// Initializes the <see cref="DAOFactoryMethod"/> class.
        /// </summary>
        static DAOFactoryMethod()
        {
            string currentDataProvider = ConfigurationManager.AppSettings["dataProvider"];
            if (string.IsNullOrWhiteSpace(currentDataProvider))
            {
                CurrentDAOFactoryValue = null;
            }
            else
            {
                switch (currentDataProvider.ToLower().Trim())
                {
                    case "sqlserver":
                        CurrentDAOFactoryValue = new SQLServerDAOFactory();
                        break;
                    case "oracle":
                        CurrentDAOFactoryValue = null;
                        return;
                    default:
                        CurrentDAOFactoryValue = new SQLServerDAOFactory();
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the current DAO factory.
        /// </summary>
        /// <value>
        /// The current DAO factory.
        /// </value>
        public static IDAOFactory CurrentDAOFactory
        {
            get
            {
                return CurrentDAOFactoryValue;
            }
        }
    }
}
