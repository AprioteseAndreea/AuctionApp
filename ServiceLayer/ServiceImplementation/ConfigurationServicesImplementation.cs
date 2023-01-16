// <copyright file="ConfigurationServicesImplementation.cs" company="PlaceholderCompany">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace ServiceLayer.ServiceImplementation
{
    using System.Collections.Generic;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using DomainModel.DTO;
    using log4net;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using ServiceLayer.Utils;

    public class ConfigurationServicesImplementation : IConfigurationServices
    {
        private readonly ILog log;
        private readonly IConfigurationDataServices configurationDataServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationServicesImplementation"/> class.
        /// </summary>
        /// <param name="configurationDataServices">The configuration data services.</param>
        /// <param name="log">The log.</param>
        public ConfigurationServicesImplementation(
            IConfigurationDataServices configurationDataServices, ILog log)
        {
            this.configurationDataServices = configurationDataServices;
            this.log = log;
        }

        /// <summary>
        /// Adds the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void AddConfiguration(ConfigurationDTO configuration)
        {
            this.ValidateConfiguration(configuration);
            this.configurationDataServices.AddConfiguration(this.GetConfigurationFromConfigurationDTO(configuration));
        }

        /// <summary>
        /// Deletes the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <exception cref="ServiceLayer.Utils.ObjectNotFoundException"></exception>
        public void DeleteConfiguration(ConfigurationDTO configuration)
        {
            this.log.Info("In DeleteCategory method");
            this.ValidateConfiguration(configuration);

            var currentConfiguration = this.configurationDataServices.GetConfigurationById(configuration.Id);
            if (currentConfiguration == null)
            {
                this.log.Warn("The configuration that you want to delete can not be found!");
                throw new ObjectNotFoundException(string.Empty);
            }

            this.log.Info("The configuration have been deleted!");
            this.configurationDataServices.DeleteConfiguration(this.GetConfigurationFromConfigurationDTO(configuration));
        }

        /// <summary>
        /// Gets the configuration by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="ServiceLayer.Utils.IncorrectIdException"></exception>
        public ConfigurationDTO GetConfigurationById(int id)
        {
            this.log.Info("In GetConfigurationById method");

            if (id < 0 || id == 0)
            {
                this.log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();
            }

            this.log.Info("The function GetConfigurationById was successfully called.");
            return new ConfigurationDTO(this.configurationDataServices.GetConfigurationById(id));
        }

        /// <summary>
        /// Gets the list of configuration.
        /// </summary>
        /// <returns></returns>
        public IList<ConfigurationDTO> GetListOfConfiguration()
        {
            this.log.Info("In GetListOfConfiguration method");
            return this.configurationDataServices.GetListOfConfiguration().Select(c => new ConfigurationDTO(c)).ToList();
        }

        /// <summary>
        /// Updates the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <exception cref="ServiceLayer.Utils.ObjectNotFoundException"></exception>
        public void UpdateConfiguration(ConfigurationDTO configuration)
        {
            this.log.Info("In UpdateConfiguration method");

            this.ValidateConfiguration(configuration);

            var currentConfiguration = this.configurationDataServices.GetConfigurationById(configuration.Id);
            if (currentConfiguration == null)
            {
                this.log.Warn("The ObjectNotFoundException was thrown!");
                throw new ObjectNotFoundException(string.Empty);
            }

            this.log.Info("The function UpdateConfiguration was successfully called.");
            this.configurationDataServices.UpdateConfiguration(this.GetConfigurationFromConfigurationDTO(configuration));
        }

        /// <summary>
        /// Validates the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <exception cref="ServiceLayer.Utils.InvalidObjectException"></exception>
        private void ValidateConfiguration(ConfigurationDTO configuration)
        {
            ValidationResults validationResults = Validation.Validate(configuration);
            if (validationResults.Count != 0)
            {
                throw new InvalidObjectException();
            }
        }

        /// <summary>
        /// Gets the configuration from configuration dto.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        private Configuration GetConfigurationFromConfigurationDTO(ConfigurationDTO configuration)
        {
            Configuration currentConfiguration = new Configuration
            {
                Id = configuration.Id,
                MaxAuctions = configuration.MaxAuctions,
                InitialScore = configuration.InitialScore,
                MinScore = configuration.MinScore,
                Days = configuration.Days,
            };

            return currentConfiguration;
        }
    }
}
