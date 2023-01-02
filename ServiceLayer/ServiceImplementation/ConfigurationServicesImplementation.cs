using DataMapper;
using DataMapper.SqlServerDAO;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImplementation
{
    public class ConfigurationServicesImplementation : IConfigurationServices
    {
        IConfigurationDataServices configurationDataServices = new SQLConfigurationDataServices();
        public void AddConfiguration(Configuration configuration)
        {
            configurationDataServices.AddConfiguration(configuration);
        }

        public void DeleteConfiguration(Configuration configuration)
        {
            configurationDataServices.DeleteConfiguration(configuration);
        }

        public Configuration GetConfigurationById(int id)
        {
            return configurationDataServices.GetConfigurationById(id);
        }

        public IList<Configuration> GetListOfConfiguration()
        {
            return configurationDataServices.GetListOfConfiguration();
        }

        public void UpdateConfiguration(Configuration configuration)
        {
            configurationDataServices.UpdateConfiguration(configuration);
        }
    }
}
