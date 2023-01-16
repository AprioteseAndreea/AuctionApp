namespace DomainModel
{
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    public class UserAuction
    {
        public int Id { get; set; }

        [NotNullValidator]
        public Product Product { get; set; }

        [NotNullValidator]
        public User User { get; set; }

        [NotNullValidator]
        public Money Price { get; set; }
    }
}