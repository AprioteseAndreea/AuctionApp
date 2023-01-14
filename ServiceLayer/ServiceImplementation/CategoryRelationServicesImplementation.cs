using DataMapper;
using DomainModel;
using DomainModel.DTO;
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

        public void AddCategoryRelation(CategoryRelationDTO category)
        {
            ValidateCategoryRelation(category);
            CheckIfChildAndParentCategoryExist(category);

            categoryRelationServices.AddCategoryRelation(GetCategoryFromCategoryDto(category));

        }
        private CategoryRelation GetCategoryFromCategoryDto(CategoryRelationDTO category)
        {
            var childCategory = categoryDataServices.GetCategoryById(category.ChildCategoryId);
            var parentCategory = categoryDataServices.GetCategoryById(category.ParentCategoryId);

            CategoryRelation currentCategory = new CategoryRelation
            {

                ParentCategory = parentCategory,
                ChildCategory = childCategory,
            };

            return currentCategory;
        }
        public void CheckIfChildAndParentCategoryExist(CategoryRelationDTO category)
        {
            var childCategory = categoryDataServices.GetCategoryById(category.ChildCategoryId);
            var parentCategory = categoryDataServices.GetCategoryById(category.ParentCategoryId);

            if (childCategory == null || parentCategory == null) throw new ObjectNotFoundException("");

        }

        public void DeleteCategoryRelation(CategoryRelationDTO category)
        {
            log.Info("In DeleteCategoryRelation method");

            ValidateCategoryRelation(category);

            var currentCategory = categoryRelationServices
                .GetCategoryRelationByChildAndParentId(category.ChildCategoryId, category.ParentCategoryId);

            if (currentCategory == null)
            {
                log.Warn("The category relation that you want to delete can not be found!");
                throw new ObjectNotFoundException("");

            }

            log.Info("The category relation have been deleted!");
            categoryRelationServices.DeleteCategoryRelation(GetCategoryFromCategoryDto(category));


        }

        public IList<CategoryRelationDTO> GetCategoryRelationByParentId(int id)
        {
            log.Info("In GetCategoryRelationByParentId method");

            if (id < 0 || id == 0)
            {
                log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();

            }

            log.Info("The function GetCategoryRelationByParentId was successfully called.");
            return categoryRelationServices.GetCategoryRelationByParentId(id)
                .Select(c => new CategoryRelationDTO(c)).ToList();


        }

        public IList<CategoryRelationDTO> GetListOfCategoriesRelation()
        {
            log.Info("In GetListOfCategoriesRelation method");
            return categoryRelationServices.GetListOfCategoriesRelation()
                .Select(c => new CategoryRelationDTO(c)).ToList();
        }

        public void UpdateCategoryRelation(CategoryRelationDTO category)
        {
            log.Info("In UpdateCategoryRelation method");

            ValidateCategoryRelation(category);

            var currentCategory = categoryRelationServices
                .GetCategoryRelationByChildAndParentId(category.ChildCategoryId, category.ParentCategoryId);
            if (currentCategory == null)
            {
                log.Warn("The ObjectNotFoundException was thrown!");
                throw new ObjectNotFoundException("");

            }

            log.Info("The function UpdateCategoryRelation was successfully called.");
            categoryRelationServices.UpdateCategoryRelation(GetCategoryFromCategoryDto(category));

        }

        public CategoryRelationDTO GetCategoryRelationByChildAndParentId(int parentId, int childId)
        {
            log.Info("In GetCategoryRelationById method");

            if (parentId < 0 || childId == 0)
            {
                log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();

            }

            log.Info("The function GetCategoryById was successfully called.");
            return new CategoryRelationDTO(categoryRelationServices.GetCategoryRelationByChildAndParentId(parentId, childId));


        }

        public IList<CategoryRelationDTO> GetCategoryRelationByChildId(int id)
        {
            log.Info("In GetCategoryRelationByChildId method");

            if (id < 0 || id == 0)
            {
                log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();

            }

            log.Info("The function GetCategoryRelationByChildId was successfully called.");
            return categoryRelationServices
                .GetCategoryRelationByChildId(id).Select(c => new CategoryRelationDTO(c)).ToList();
        }
        private void ValidateCategoryRelation(CategoryRelationDTO category)
        {
            ValidationResults validationResults = Validation.Validate(category);
            if (validationResults.Count != 0) throw new InvalidObjectException();
        }
    }
}
