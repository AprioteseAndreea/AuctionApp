namespace ServiceLayer
{
    using System.Collections.Generic;
    using DomainModel.DTO;

    public interface IConfigurationServices
    {
        /// <summary>
        /// Gets the list of configuration.
        /// </summary>
        /// <returns></returns>
        IList<ConfigurationDTO> GetListOfConfiguration();

        /// <summary>
        /// Gets the configuration by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ConfigurationDTO GetConfigurationById(int id);

        /// <summary>
        /// Deletes the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        void DeleteConfiguration(ConfigurationDTO configuration);

        /// <summary>
        /// Updates the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        void UpdateConfiguration(ConfigurationDTO configuration);

        /// <summary>
        /// Adds the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        void AddConfiguration(ConfigurationDTO configuration);
    }
}
