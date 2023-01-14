using DomainModel.DTO;
using System.Collections.Generic;

namespace ServiceLayer
{
   public interface ICategoryServices
    {
        IList<CategoryDTO> GetListOfCategories();

        void DeleteCategory(CategoryDTO category);

        void UpdateCategory(CategoryDTO category);

        CategoryDTO GetCategoryById(int id);

        void AddCategory(CategoryDTO category);
    }
}
