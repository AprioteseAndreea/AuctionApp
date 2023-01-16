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
        public ProductDTO() { }

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

        public int Id { get; set; }

        [NotNullValidator(MessageTemplate = "The name cannot be null")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The name should have between {3} and {5} letters")]
        public string Name { get; set; }

        [NotNullValidator(MessageTemplate = "The description cannot be null")]
        [StringLengthValidator(3, 100)]
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int OwnerUserId { get; set; }

        [NotNullValidator]
        public DateTime StartDate { get; set; }

        [NotNullValidator]
        public DateTime EndDate { get; set; }

        [NotNullValidator(MessageTemplate = "The sell price cannot be null")]
        public Money StartingPrice { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int CategoryId { get; set; }

        [NotNullValidator]
        [EnumDataType(typeof(AuctionStatus))]
        public AuctionStatus Status { get; set; }

        [SelfValidation]
        public void ValidateStartAndEndDate(ValidationResults validationResults)
        {
            if (this.EndDate < this.StartDate)
            {
                validationResults.AddResult(new ValidationResult("The end date should not be less than start date", this, "ValidateStartAndEndDate", "error", null));
            }
        }

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