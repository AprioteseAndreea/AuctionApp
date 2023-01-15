using DomainModel;
using System.Collections.Generic;

namespace DataMapper
{
    public interface IConfigurationDataServices
    {
        IList<Configuration> GetListOfConfiguration();
        Configuration GetConfigurationById(int id);
        void DeleteConfiguration(Configuration configuration);
        void UpdateConfiguration(Configuration configuration);
        void AddConfiguration(Configuration configuration);
    }
}
