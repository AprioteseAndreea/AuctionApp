using System.ComponentModel.DataAnnotations;


namespace DomainModel
{
    public class Configuration
    {
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
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]

        public int InitialScore
        {
            get;
            set;
        }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]

        public int MinScore
        {
            get;
            set;
        }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]

        public int Days
        {
            get;
            set;
        }
    }
}
