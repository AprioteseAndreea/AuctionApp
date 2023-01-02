using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IConfigurationServices
    {
        IList<Configuration> GetListOfConfiguration();
        Configuration GetConfigurationById(int id);

        void DeleteConfiguration(Configuration configuration);

        void UpdateConfiguration(Configuration configuration);

        void AddConfiguration(Configuration configuration);
    }
}
