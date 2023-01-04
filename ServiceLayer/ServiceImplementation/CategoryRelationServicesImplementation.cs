using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImplementation
{
    public class CategoryRelationServicesImplementation : ICategoryRelationServices
    {
        private readonly ICategoryRelationServices categoryRelationServices;
        public CategoryRelationServicesImplementation(ICategoryRelationServices categoryRelationServices)
        {
            this.categoryRelationServices = categoryRelationServices;
        }

        public void AddCategoryRelation(CategoryRelation category)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategoryRelation(CategoryRelation category)
        {
            throw new NotImplementedException();
        }

        public IList<CategoryRelation> GetCategoryRelationByParentId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<CategoryRelation> GetListOfCategoriesRelation()
        {
            throw new NotImplementedException();
        }

        public void UpdateCategoryRelation(CategoryRelation category)
        {
            throw new NotImplementedException();
        }
    }
}
