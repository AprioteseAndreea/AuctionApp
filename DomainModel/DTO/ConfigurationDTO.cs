namespace DomainModel.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class ConfigurationDTO
    {
        public ConfigurationDTO()
        {
        }

        public ConfigurationDTO(Configuration configuration)
        {
            this.Id = configuration.Id;
            this.MaxAuctions = configuration.MaxAuctions;
            this.InitialScore = configuration.InitialScore;
            this.MinScore = configuration.MinScore;
            this.Days = configuration.Days;
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