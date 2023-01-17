// <copyright file="SQLConfigurationDataServices.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System.Collections.Generic;
    using System.Linq;
    using DomainModel;

    public class SQLConfigurationDataServices : IConfigurationDataServices
    {
        /// <summary>
        /// Adds the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void AddConfiguration(Configuration configuration)
        {
            using (var context = new MyApplicationContext())
            {
                context.Configurations.Add(configuration);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void DeleteConfiguration(Configuration configuration)
        {
            using (var context = new MyApplicationContext())
            {
                var newConfig = new Configuration { Id = configuration.Id };
                context.Configurations.Attach(newConfig);
                context.Configurations.Remove(newConfig);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the configuration by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Configuration GetConfigurationById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Configurations.Where(config => config.Id == id).SingleOrDefault();
            }
        }

        /// <summary>
        /// Gets the list of configuration.
        /// </summary>
        /// <returns></returns>
        public IList<Configuration> GetListOfConfiguration()
        {
            using (var context = new MyApplicationContext())
            {
                return context.Configurations.Select(c => c).ToList();
            }
        }

        /// <summary>
        /// Updates the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void UpdateConfiguration(Configuration configuration)
        {
            using (var context = new MyApplicationContext())
            {
                var result = context.Configurations.First(c => c.Id == configuration.Id);
                if (result != null)
                {
                    result.MaxAuctions = configuration.MaxAuctions;
                    result.InitialScore = configuration.InitialScore;
                    result.MinScore = configuration.MinScore;
                    result.Days = configuration.Days;
                    context.SaveChanges();
                }
            }
        }
    }
}