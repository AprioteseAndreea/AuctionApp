// <copyright file="UserAuctionDTO.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DomainModel.DTO
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    public class UserAuctionDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuctionDTO"/> class.
        /// </summary>
        public UserAuctionDTO()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuctionDTO"/> class.
        /// </summary>
        /// <param name="userAuction">The user auction.</param>
        public UserAuctionDTO(UserAuction userAuction)
        {
            this.Id = userAuction.Id;
            this.ProductId = userAuction.Product.Id;
            this.UserId = userAuction.Id;
            this.Price = userAuction.Price;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [NotNullValidator]
        public Money Price { get; set; }
    }
}