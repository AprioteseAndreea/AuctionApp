using DomainModel.enums;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.ComponentModel.DataAnnotations;
using ValidationResult = Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult;

namespace DomainModel
{
    [HasSelfValidation]
    public class Product
    {

        public int Id { get; set; }

        [NotNullValidator(MessageTemplate = "The name cannot be null")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The name should have between {3} and {5} letters")]
        public string Name { get; set; }

        [NotNullValidator(MessageTemplate = "The description cannot be null")]
        [StringLengthValidator(3, 100)]
        public string Description { get; set; }

        [NotNullValidator]
        public User OwnerUser { get; set; }

        [NotNullValidator]
        public DateTime StartDate { get; set; }

        [NotNullValidator]
        public DateTime EndDate { get; set; }

        [NotNullValidator(MessageTemplate = "The sell price cannot be null")]
        public Money StartingPrice { get; set; }

        [NotNullValidator]
        public Category Category { get; set; }

        [NotNullValidator]
        [EnumDataType(typeof(AuctionStatus))]
        public AuctionStatus Status { get; set; }

        [SelfValidation]
        public void ValidateStartAndEndDate(ValidationResults validationResults)
        {
            if (EndDate < StartDate)
            {
                validationResults.AddResult(new ValidationResult("The end date should not be less than start date", this, "ValidateStartAndEndDate", "error", null));

            }
        }

        [SelfValidation]
        public void ValidateStartDate(ValidationResults validationResults)
        {
            if (StartDate < DateTime.Now.Date)
            {
                validationResults.AddResult(new ValidationResult("The start date should not be less than current date", this, "ValidateStartDate", "error", null));

            }
        }
    }
}
