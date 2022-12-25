using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    [HasSelfValidation]
    public class Product
    {
        public int Id
        {
            get;
            set;
        }

        [ObjectValidator]
        [NotNullValidator(MessageTemplate = "The name cannot be null")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The name should have between {3} and {5} letters")]
        public string Name
        {
            get;
            set;
        }

        [ObjectValidator]
        [StringLengthValidator(3, 100)]
        public string Description
        {
            get;
            set;
        }

        [ObjectValidator]
        [NotNullValidator]
        public User OwnerUser
        {
            get;
            set;
        }

        [ObjectValidator]
        [NotNullValidator]
        public DateTime StartDate
        {
            get;
            set;
        }

        [ObjectValidator]
        [NotNullValidator]
        public DateTime EndDate
        {
            get;
            set;
        }

        [ObjectValidator]
        [NotNullValidator(MessageTemplate = "The sell price cannot be null")]
        public Money StartingPrice
        {
            get;
            set;
        }

        [ObjectValidator]
        [NotNullValidator]
        public Category Category
        {
            get;
            set;
        }

        [ObjectValidator]
        [DomainValidator("Opened", "Closed", MessageTemplate = "Unknown status")]
        public string Status
        {
            get;
            set;
        }

        [SelfValidation]
        public void ValidatePrice(ValidationResults validationResults)
        {
            if (EndDate < StartDate)
            {
                validationResults.AddResult(new ValidationResult("The end date should not be less than start date", this, "ValidatePrice", "error", null));

            }
        }
    }
}
