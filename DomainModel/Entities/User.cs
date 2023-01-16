// <copyright file="User.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.Enums;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using ValidationResult = Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult;

    [HasSelfValidation]
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [NotNullValidator(MessageTemplate = "The first name cannot be null")]
        [RegexValidator("[A-Za-z]")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The name should have between {3} and {5} letters")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [NotNullValidator(MessageTemplate = "The last name cannot be null")]
        [RegexValidator("[A-Za-z]")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The name should have between {3} and {5} letters")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [NotNullValidator(MessageTemplate = "The email cannot be null")]
        [RegexValidator(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        [NotNullValidator]
        public string BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        [NotNullValidator]
        [Range(1, 5, ErrorMessage = "Please enter a value bigger than {1}")]
        public double Score { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [NotNullValidator]
        [EnumDataType(typeof(UserStatus))]
        public UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        [NotNullValidator]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        /// <summary>
        /// Validates the specified validation results.
        /// </summary>
        /// <param name="validationResults">The validation results.</param>
        [SelfValidation]
        public void Validate(ValidationResults validationResults)
        {
            var age = DateTime.Now.Year - DateTime.Parse(this.BirthDate).Year;
            if (age < 18)
            {
                validationResults.AddResult(
                    new ValidationResult(
                        "Customer must be older than 18",
                        this,
                        "BirthDate",
                        null,
                        null));
            }

            if (DateTime.Now < DateTime.Parse(this.BirthDate))
            {
                validationResults.AddResult(
                  new ValidationResult(
                      "You can not set a birth date in the future!",
                      this,
                      "BirthDate",
                      null,
                      null));
            }
        }
    }
}