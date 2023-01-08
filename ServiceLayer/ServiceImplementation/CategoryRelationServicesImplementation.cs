using DataMapper;
using DomainModel;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using ServiceLayer.Utils;
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
        private readonly ICategoryDataServices categoryDataServices;
        private static readonly ILog log = LogManager.GetLogger(typeof(CategoryServicesImplementation));
 
        public CategoryRelationServicesImplementation(ICategoryRelationServices categoryRelationServices)
        {
            this.categoryRelationServices = categoryRelationServices;
        }

        public void AddCategoryRelation(CategoryRelation category)
        {
            log.Info("In AddCategoryRelation method.");
            ValidationResults validationResults = Validation.Validate(category);
            if (validationResults.Count == 0)
            {
                var childCategory = categoryDataServices.GetCategoryById(category.ChildCategory.Id);
                var parentCategory = categoryDataServices.GetCategoryById(category.ParentCategory.Id);
                if(childCategory != null && parentCategory != null)
                {
                    categoryRelationServices.AddCategoryRelation(category);

                }
                else
                {
                    throw new ObjectNotFoundException("");
                }
            }
            else
            {
                throw new InvalidObjectException();

            }
        }

        public void DeleteCategoryRelation(CategoryRelation category)
        {

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

        public CategoryRelation GetCategoryRelationById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<CategoryRelation> GetCategoryRelationByChildId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
