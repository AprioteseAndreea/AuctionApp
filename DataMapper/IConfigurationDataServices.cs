namespace DataMapper
{
    using System.Collections.Generic;
    using DomainModel;

    public interface IConfigurationDataServices
    {
        IList<Configuration> GetListOfConfiguration();

        Configuration GetConfigurationById(int id);

        void DeleteConfiguration(Configuration configuration);

        void UpdateConfiguration(Configuration configuration);

        void AddConfiguration(Configuration configuration);
    }
}
