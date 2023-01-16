// <copyright file="ProductDTO.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DomainModel.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.Enums;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using ValidationResult = Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult;

    [HasSelfValidation]
    public class ProductDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDTO"/> class.
        /// </summary>
        public ProductDTO()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDTO"/> class.
        /// </summary>
        /// <param name="product">The product.</param>
        public ProductDTO(Product product)
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.Description = product.Description;
            this.OwnerUserId = product.OwnerUser.Id;
            this.StartDate = product.StartDate;
            this.EndDate = product.EndDate;
            this.StartingPrice = product.StartingPrice;
            this.CategoryId = product.Category.Id;
            this.Status = product.Status;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [NotNullValidator(MessageTemplate = "The name cannot be null")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The name should have between {3} and {5} letters")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [NotNullValidator(MessageTemplate = "The description cannot be null")]
        [StringLengthValidator(3, 100)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the owner user identifier.
        /// </summary>
        /// <value>
        /// The owner user identifier.
        /// </value>
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int OwnerUserId { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        [NotNullValidator]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        [NotNullValidator]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the starting price.
        /// </summary>
        /// <value>
        /// The starting price.
        /// </value>
        [NotNullValidator(MessageTemplate = "The sell price cannot be null")]
        public Money StartingPrice { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [NotNullValidator]
        [EnumDataType(typeof(AuctionStatus))]
        public AuctionStatus Status { get; set; }

        /// <summary>
        /// Validates the start and end date.
        /// </summary>
        /// <param name="validationResults">The validation results.</param>
        [SelfValidation]
        public void ValidateStartAndEndDate(ValidationResults validationResults)
        {
            if (this.EndDate < this.StartDate)
            {
                validationResults.AddResult(new ValidationResult("The end date should not be less than start date", this, "ValidateStartAndEndDate", "error", null));
            }
        }

        /// <summary>
        /// Validates the start date.
        /// </summary>
        /// <param name="validationResults">The validation results.</param>
        [SelfValidation]
        public void ValidateStartDate(ValidationResults validationResults)
        {
            if (this.StartDate < DateTime.Now.Date)
            {
                validationResults.AddResult(new ValidationResult("The start date should not be less than current date", this, "ValidateStartDate", "error", null));
            }
        }
    }
}