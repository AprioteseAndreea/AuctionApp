using DomainModel.enums;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.ComponentModel.DataAnnotations;
using ValidationResult = Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult;


namespace DomainModel.DTO
{
    [HasSelfValidation]
    public class UserDTO
    {

        public UserDTO() { }
        public UserDTO(User user)
        {

            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            BirthDate = user.BirthDate;
            Score = user.Score;
            Status = user.Status;
        }

        public int Id { get; set; }

        [NotNullValidator(MessageTemplate = "The name cannot be null")]
        [RegexValidator("[A-Za-z]")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The name should have between {3} and {5} letters")]
        public string FirstName { get; set; }

        [NotNullValidator(MessageTemplate = "The name cannot be null")]
        [RegexValidator("[A-Za-z]")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The name should have between {3} and {5} letters")]
        public string LastName { get; set; }

        [NotNullValidator(MessageTemplate = "The email cannot be null")]
        [RegexValidator(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$")]
        public string Email { get; set; }

        [NotNullValidator]
        public string BirthDate { get; set; }

        [NotNullValidator]
        [Range(1, 5, ErrorMessage = "Please enter a value bigger than {1}")]
        public double Score { get; set; }

        [NotNullValidator]
        [EnumDataType(typeof(UserStatus))]
        public UserStatus Status { get; set; }

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
