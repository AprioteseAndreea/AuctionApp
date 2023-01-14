using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.DTO
{
    public class ConfigurationDTO
    {
        public ConfigurationDTO() { }
        public ConfigurationDTO(Configuration configuration)
        {

            Id = configuration.Id;
            MaxAuctions = configuration.MaxAuctions;
            InitialScore = configuration.InitialScore;
            MinScore = configuration.MinScore;
            Days = configuration.Days;

        }

        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int MaxAuctions { get; set; }

        [Range(1, 5, ErrorMessage = "Please enter a value bigger than {1}")]
        public double InitialScore { get; set; }

        [Range(1, 5, ErrorMessage = "Please enter a value bigger than {1}")]
        public double MinScore { get; set; }

        [Range(1, 365, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Days { get; set; }
    }
}
