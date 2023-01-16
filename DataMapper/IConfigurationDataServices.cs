// <copyright file="IConfigurationDataServices.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DataMapper
{
    using System.Collections.Generic;
    using DomainModel;

    public interface IConfigurationDataServices
    {
        /// <summary>
        /// Gets the list of configuration.
        /// </summary>
        /// <returns></returns>
        IList<Configuration> GetListOfConfiguration();

        /// <summary>
        /// Gets the configuration by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Configuration GetConfigurationById(int id);

        /// <summary>
        /// Deletes the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        void DeleteConfiguration(Configuration configuration);

        /// <summary>
        /// Updates the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        void UpdateConfiguration(Configuration configuration);

        /// <summary>
        /// Adds the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        void AddConfiguration(Configuration configuration);
    }
}
