namespace DomainModel.DTO
{
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    public class CategoryDTO
    {
        public CategoryDTO() { }

        public CategoryDTO(Category category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
        }

        public int Id { get; set; }

        [NotNullValidator(MessageTemplate = "The name cannot be null")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The name should have between {3} and {5} letters")]
        public string Name { get; set; }
    }
}