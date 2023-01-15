using DataMapper;
using DomainModel;
using DomainModel.DTO;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using ServiceLayer.Utils;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.ServiceImplementation
{
    public class CategoryServicesImplementation : ICategoryServices
    {
        private readonly ILog log;
        private readonly ICategoryDataServices categoryDataServices;
        public CategoryServicesImplementation(
            ICategoryDataServices categoryDataServices,
            ILog log)
        {
            this.categoryDataServices = categoryDataServices;
            this.log = log;
        }

        public void AddCategory(CategoryDTO category)
        {
            ValidateCategory(category);
            categoryDataServices.AddCategory(GetCategoryFromCategoryDto(category));
        }
        private Category GetCategoryFromCategoryDto(CategoryDTO category)
        {
            Category currentCategory = new Category
            {
                Id = category.Id,
                Name = category.Name,
            };

            return currentCategory;
        }
        private void ValidateCategory(CategoryDTO category)
        {
            ValidationResults validationResults = Validation.Validate(category);
            if (validationResults.Count != 0) throw new InvalidObjectException();
        }
        public void DeleteCategory(CategoryDTO category)
        {
            log.Info("In DeleteCategory method");
            ValidateCategory(category);

            var currentCategory = categoryDataServices.GetCategoryById(category.Id);
            if (currentCategory == null)
            {
                log.Warn("The user that you want to delete can not be found!");
                throw new ObjectNotFoundException(category.Name);
            }

            log.Info("The user have been deleted!");
            categoryDataServices.DeleteCategory(GetCategoryFromCategoryDto(category));
        }

        public CategoryDTO GetCategoryById(int id)
        {
            log.Info("In GetCategoryById method");

            if (id < 0 || id == 0)
            {
                log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();

            }

            log.Info("The function GetCategoryById was successfully called.");
            return new CategoryDTO(categoryDataServices.GetCategoryById(id));

        }

        public IList<CategoryDTO> GetListOfCategories()
        {
            log.Info("In GetListOfCategories method");
            return categoryDataServices.GetListOfCategories().Select(c => new CategoryDTO(c)).ToList();

        }

        public void UpdateCategory(CategoryDTO category)
        {
            log.Info("In UpdateCategory method");
            ValidateCategory(category);

            var currentCategory = categoryDataServices.GetCategoryById(category.Id);
            if (currentCategory == null)
            {
                log.Warn("The ObjectNotFoundException was thrown!");
                throw new ObjectNotFoundException(category.Name);

            }

            log.Info("The function UpdateCategory was successfully called.");
            categoryDataServices.UpdateCategory(GetCategoryFromCategoryDto(category));
        }
    }
}
