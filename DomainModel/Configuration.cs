using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.ComponentModel.DataAnnotations;


namespace DomainModel
{
    public class Configuration
    {
        public Configuration()
        {

        }
        public int Id
        {
            get;
            set;
        }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int MaxAuctions
        {
            get;
            set;
        }
        [NotNullValidator]
        [Range(1, 5, ErrorMessage = "Please enter a value bigger than {1}")]
        public double InitialScore
        {
            get;
            set;
        }

        [NotNullValidator]
        [Range(1, 5, ErrorMessage = "Please enter a value bigger than {1}")]
        public double MinScore
        {
            get;
            set;
        }

        [NotNullValidator]
        [Range(1, 365, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Days
        {
            get;
            set;
        }
    }
}
