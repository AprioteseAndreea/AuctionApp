namespace DomainModel.DTO
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    public class UserAuctionDTO
    {
        public UserAuctionDTO()
        {
        }

        public UserAuctionDTO(UserAuction userAuction)
        {
            this.Id = userAuction.Id;
            this.ProductId = userAuction.Product.Id;
            this.UserId = userAuction.Id;
            this.Price = userAuction.Price;
        }

        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int UserId { get; set; }

        [NotNullValidator]
        public Money Price { get; set; }
    }
}