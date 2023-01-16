// <copyright file="Product.cs" company="Transilvania University of Brasov">
// Copyright (c) Transilvania University of Brasov. All rights reserved.
// </copyright>

namespace DomainModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.Enums;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using ValidationResult = Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult;

    [HasSelfValidation]
    public class Product
    {
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
        /// Gets or sets the owner user.
        /// </summary>
        /// <value>
        /// The owner user.
        /// </value>
        [NotNullValidator]
        public User OwnerUser { get; set; }

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
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [NotNullValidator]
        public Category Category { get; set; }

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
                validationResults.AddResult(
                    new ValidationResult(
                        "The end date should not be less than start date",
                        this,
                        "ValidateStartAndEndDate",
                        "error",
                        null));
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
                validationResults.AddResult(
                    new ValidationResult(
                        "The start date should not be less than current date",
                        this,
                        "ValidateStartDate",
                        "error",
                        null));
            }
        }
    }
}
