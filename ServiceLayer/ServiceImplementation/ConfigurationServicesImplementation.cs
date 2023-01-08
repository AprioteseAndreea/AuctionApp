using DataMapper;
using DataMapper.SqlServerDAO;
using DomainModel;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using ServiceLayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void AddConfiguration(Configuration configuration)
        {
            log.Info("In AddConfiguration method.");
            ValidationResults validationResults = Validation.Validate(configuration);
            if (validationResults.Count == 0)
            {
                configurationDataServices.AddConfiguration(configuration);
            }
            else
            {
                throw new InvalidObjectException();

            }
        }

        public void DeleteConfiguration(Configuration configuration)
        {
            log.Info("In DeleteCategory method");

            if (configuration != null)
            {
                var currentConfiguration = configurationDataServices.GetConfigurationById(configuration.Id);
                if (currentConfiguration != null)
                {
                    log.Info("The configuration have been deleted!");
                    configurationDataServices.DeleteConfiguration(configuration);
                }
                else
                {
                    log.Warn("The configuration that you want to delete can not be found!");
                    throw new ObjectNotFoundException("");
                }
            }
            else
            {
                log.Warn("The object passed by parameter is null.");
                throw new NullReferenceException("The object can not be null.");
            }
        }

        public Configuration GetConfigurationById(int id)
        {
            log.Info("In GetConfigurationById method");

            if (id < 0 || id == 0)
            {
                log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();

            }
            else
            {
                log.Info("The function GetConfigurationById was successfully called.");
                return configurationDataServices.GetConfigurationById(id);

            }
        }

        public IList<Configuration> GetListOfConfiguration()
        {
            log.Info("In GetListOfConfiguration method");
            return configurationDataServices.GetListOfConfiguration();
        }

        public void UpdateConfiguration(Configuration configuration)
        {

            log.Info("In UpdateConfiguration method");

            if (configuration != null)
            {
                var currentConfiguration = configurationDataServices.GetConfigurationById(configuration.Id);
                if (currentConfiguration != null)
                {
                    log.Info("The function UpdateConfiguration was successfully called.");
                    configurationDataServices.UpdateConfiguration(configuration);

                }
                else
                {
                    log.Warn("The ObjectNotFoundException was thrown!");
                    throw new ObjectNotFoundException("");
                }
            }
            else
            {
                log.Warn("The NullReferenceException was thrown!");
                throw new NullReferenceException("The object can not be null.");
            }
        }
    }
}
