using DomainModel;
using System.Collections.Generic;

namespace DataMapper
{
    public interface ICategoryRelationDataServices
    {
        IList<CategoryRelation> GetListOfCategoriesRelation();
        void DeleteCategoryRelation(CategoryRelation category);
        CategoryRelation GetCategoryRelationByChildAndParentId(int parentId, int childId);
        void UpdateCategoryRelation(CategoryRelation category);
        IList<CategoryRelation> GetCategoryRelationByParentId(int id);
        IList<CategoryRelation> GetCategoryRelationByChildId(int id);
        void AddCategoryRelation(CategoryRelation category);
    }
}
