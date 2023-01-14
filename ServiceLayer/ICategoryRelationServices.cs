using DomainModel;
using DomainModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
   public interface ICategoryRelationServices
    {
        IList<CategoryRelationDTO> GetListOfCategoriesRelation();

        void DeleteCategoryRelation(CategoryRelationDTO category);
        CategoryRelationDTO GetCategoryRelationByChildAndParentId(int parentId, int childId);


        void UpdateCategoryRelation(CategoryRelationDTO category);

        IList<CategoryRelationDTO> GetCategoryRelationByParentId(int id);
        IList<CategoryRelationDTO> GetCategoryRelationByChildId(int id);


        void AddCategoryRelation(CategoryRelationDTO category);
    }
}
