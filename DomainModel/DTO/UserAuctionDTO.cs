using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.DTO
{
    public class UserAuctionDTO
    {
        public UserAuctionDTO() { }
        public UserAuctionDTO(UserAuction userAuction)
        {

            Id = userAuction.Id;
            ProductId = userAuction.Product.Id;
            UserId = userAuction.Id;
            Price = userAuction.Price;

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
