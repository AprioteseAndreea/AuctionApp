using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationResult = Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult;

namespace DomainModel
{
    [HasSelfValidation]
    public class User
    {
        public User()
        {
            Products = new List<Product>();
        }
        public int Id
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "The name cannot be null")]
        [RegexValidator("[A-Za-z]")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The name should have between {3} and {5} letters")]
        public string Name
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "The email cannot be null")]
        [RegexValidator(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$")]
        public string Email
        {
            get;
            set;
        }

        [NotNullValidator]
        public string BirthDate
        {
            get;
            set;
        }

        [NotNullValidator]
        [Range(1, 5, ErrorMessage = "Please enter a value bigger than {1}")]
        public double Score
        {
            get;
            set;
        }

        [NotNullValidator]
        [DomainValidator("Active", "Inactive", MessageTemplate = "Unknown status")]
        public string Status
        {
            get;
            set;
        }

        [NotNullValidator]
        public virtual ICollection<Product> Products
        {
            get;
            set;
        }
        [SelfValidation]
        public void Validate(ValidationResults validationResults)
        {
            var age = DateTime.Now.Year - DateTime.Parse(BirthDate).Year;
            if (age < 18)
            {

                validationResults.AddResult(
                    new ValidationResult("Customer must be older than 18",
                        this,
                        "BirthDate",
                        null,
                        null));
            }
            if (DateTime.Now < DateTime.Parse(BirthDate))
            {
                validationResults.AddResult(
                  new ValidationResult("You can not set a birth date in the future!",
                      this,
                      "BirthDate",
                      null,
                      null));
            }
        }
    }
}
