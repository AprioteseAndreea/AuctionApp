using DomainModel;
using DomainModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IConfigurationServices
    {
        IList<ConfigurationDTO> GetListOfConfiguration();
        ConfigurationDTO GetConfigurationById(int id);

        void DeleteConfiguration(ConfigurationDTO configuration);

        void UpdateConfiguration(ConfigurationDTO configuration);

        void AddConfiguration(ConfigurationDTO configuration);
    }
}
