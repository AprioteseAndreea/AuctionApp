using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.ComponentModel.DataAnnotations;

namespace DomainModel.DTO
{
    public class CategoryRelationDTO
    {
        public CategoryRelationDTO() { }
        public CategoryRelationDTO(CategoryRelation categoryRelation)
        {

            ParentCategoryId = categoryRelation.ParentCategory.Id;
            ChildCategoryId = categoryRelation.ChildCategory.Id;

        }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int ParentCategoryId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int ChildCategoryId { get; set; }
    }
}
