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
        private readonly ICategoryRelationDataServices categoryRelationServices;
        private readonly ICategoryDataServices categoryDataServices;
        private static readonly ILog log = LogManager.GetLogger(typeof(CategoryServicesImplementation));
 
        public CategoryRelationServicesImplementation(ICategoryRelationDataServices categoryRelationServices, ICategoryDataServices categoryDataServices)
        {
            this.categoryRelationServices = categoryRelationServices;
            this.categoryDataServices = categoryDataServices;
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
            log.Info("In DeleteCategoryRelation method");
            if (category != null)
            {
                var currentCategory = categoryRelationServices.GetCategoryRelationById(category.Id);
                if (currentCategory != null)
                {
                    log.Info("The category relation have been deleted!");
                    categoryRelationServices.DeleteCategoryRelation(category);
                }
                else
                {
                    log.Warn("The category relation that you want to delete can not be found!");
                    throw new ObjectNotFoundException("");
                }
            }
            else
            {
                log.Warn("The object passed by parameter is null.");
                throw new NullReferenceException("The object can not be null.");
            }

        }

        public IList<CategoryRelation> GetCategoryRelationByParentId(int id)
        {
            log.Info("In GetCategoryRelationByParentId method");

            if (id < 0 || id == 0)
            {
                log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();

            }
            else
            {
                log.Info("The function GetCategoryRelationByParentId was successfully called.");
                return categoryRelationServices.GetCategoryRelationByParentId(id);

            }
        }

        public IList<CategoryRelation> GetListOfCategoriesRelation()
        {
            log.Info("In GetListOfCategoriesRelation method");
            return categoryRelationServices.GetListOfCategoriesRelation();
        }

        public void UpdateCategoryRelation(CategoryRelation category)
        {
            log.Info("In UpdateCategoryRelation method");

            if (category != null)
            {
                var currentCategory = categoryRelationServices.GetCategoryRelationById(category.Id);
                if (currentCategory != null)
                {
                    log.Info("The function UpdateCategoryRelation was successfully called.");
                    categoryRelationServices.UpdateCategoryRelation(category);

                }
                else
                {
                    log.Warn("The ObjectNotFoundException was thrown!");
                    throw new ObjectNotFoundException("");
                }
            }
            else
            {
                log.Warn("The NullReferenceException was thrown!");
                throw new NullReferenceException("The object can not be null.");
            }
        }

        public CategoryRelation GetCategoryRelationById(int id)
        {
            log.Info("In GetCategoryRelationById method");

            if (id < 0 || id == 0)
            {
                log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();

            }
            else
            {
                log.Info("The function GetCategoryById was successfully called.");
                return categoryRelationServices.GetCategoryRelationById(id);

            }
        }

        public IList<CategoryRelation> GetCategoryRelationByChildId(int id)
        {
            log.Info("In GetCategoryRelationByChildId method");

            if (id < 0 || id == 0)
            {
                log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();

            }
            else
            {
                log.Info("The function GetCategoryRelationByChildId was successfully called.");
                return categoryRelationServices.GetCategoryRelationByChildId(id);

            }

        }
    }
}
