using DataMapper;
using DomainModel;
using DomainModel.DTO;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using ServiceLayer.Utils;
using System.Collections.Generic;
using System.Linq;


namespace ServiceLayer.ServiceImplementation
{
    public class ConfigurationServicesImplementation : IConfigurationServices
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CategoryServicesImplementation));
        private readonly IConfigurationDataServices configurationDataServices;
        public ConfigurationServicesImplementation(IConfigurationDataServices configurationDataServices)
        {
            this.configurationDataServices = configurationDataServices;
        }

        public void AddConfiguration(ConfigurationDTO configuration)
        {
            ValidateConfiguration(configuration);
            configurationDataServices.AddConfiguration(GetConfigurationFromConfigurationDTO(configuration));
        }
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
        private void ValidateConfiguration(ConfigurationDTO configuration)
        {
            ValidationResults validationResults = Validation.Validate(configuration);
            if (validationResults.Count != 0) throw new InvalidObjectException();
        }
        public void DeleteConfiguration(ConfigurationDTO configuration)
        {
            log.Info("In DeleteCategory method");
            ValidateConfiguration(configuration);

            var currentConfiguration = configurationDataServices.GetConfigurationById(configuration.Id);
            if (currentConfiguration == null)
            {
                log.Warn("The configuration that you want to delete can not be found!");
                throw new ObjectNotFoundException("");

            }

            log.Info("The configuration have been deleted!");
            configurationDataServices.DeleteConfiguration(GetConfigurationFromConfigurationDTO(configuration));
        }

        public ConfigurationDTO GetConfigurationById(int id)
        {
            log.Info("In GetConfigurationById method");

            if (id < 0 || id == 0)
            {
                log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();

            }

            log.Info("The function GetConfigurationById was successfully called.");
            return new ConfigurationDTO(configurationDataServices.GetConfigurationById(id));

        }

        public IList<ConfigurationDTO> GetListOfConfiguration()
        {
            log.Info("In GetListOfConfiguration method");
            return configurationDataServices.GetListOfConfiguration().Select(c => new ConfigurationDTO(c)).ToList();
        }

        public void UpdateConfiguration(ConfigurationDTO configuration)
        {

            log.Info("In UpdateConfiguration method");

            ValidateConfiguration(configuration);

            var currentConfiguration = configurationDataServices.GetConfigurationById(configuration.Id);
            if (currentConfiguration == null)
            {
                log.Warn("The ObjectNotFoundException was thrown!");
                throw new ObjectNotFoundException("");


            }
            log.Info("The function UpdateConfiguration was successfully called.");
            configurationDataServices.UpdateConfiguration(GetConfigurationFromConfigurationDTO(configuration));
        }
    }
}
