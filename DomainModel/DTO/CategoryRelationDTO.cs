namespace DomainModel.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryRelationDTO
    {
        public CategoryRelationDTO() { }

        public CategoryRelationDTO(CategoryRelation categoryRelation)
        {
            this.ParentCategoryId = categoryRelation.ParentCategory.Id;
            this.ChildCategoryId = categoryRelation.ChildCategory.Id;
        }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int ParentCategoryId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int ChildCategoryId { get; set; }
    }
}
