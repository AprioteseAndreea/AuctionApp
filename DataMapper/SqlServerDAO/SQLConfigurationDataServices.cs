using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SqlServerDAO
{
    public class SQLConfigurationDataServices : IConfigurationDataServices
    {
        public void AddConfiguration(Configuration configuration)
        {
            using (var context = new MyApplicationContext())
            {
                context.Configurations.Add(configuration);
                context.SaveChanges();
            }
        }

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

        public Configuration GetConfigurationById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Configurations.Where(config => config.Id == id).SingleOrDefault();
            }
        }

        public IList<Configuration> GetListOfConfiguration()
        {
            using (var context = new MyApplicationContext())
            {
                return context.Configurations.Select(c => c).ToList();
            }
        }

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
