namespace ServiceLayer.ServiceImplementation
{
    using System.Collections.Generic;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using DomainModel.DTO;
    using log4net;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using ServiceLayer.Utils;

    public class CategoryServicesImplementation : ICategoryServices
    {
        private readonly ILog log;
        private readonly ICategoryDataServices categoryDataServices;

        /// <summary>Initializes a new instance of the <see cref="CategoryServicesImplementation" /> class.</summary>
        /// <param name="categoryDataServices">The category data services.</param>
        /// <param name="log">The log.</param>
        public CategoryServicesImplementation(
            ICategoryDataServices categoryDataServices,
            ILog log)
        {
            this.categoryDataServices = categoryDataServices;
            this.log = log;
        }

        /// <summary>
        /// Adds the category.
        /// </summary>
        /// <param name="category">The category.</param>
        public void AddCategory(CategoryDTO category)
        {
            this.ValidateCategory(category);
            this.categoryDataServices.AddCategory(this.GetCategoryFromCategoryDto(category));
        }

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <exception cref="ServiceLayer.Utils.ObjectNotFoundException"></exception>
        public void DeleteCategory(CategoryDTO category)
        {
            this.log.Info("In DeleteCategory method");
            this.ValidateCategory(category);

            var currentCategory = this.categoryDataServices.GetCategoryById(category.Id);
            if (currentCategory == null)
            {
                this.log.Warn("The user that you want to delete can not be found!");
                throw new ObjectNotFoundException(category.Name);
            }

            this.log.Info("The user have been deleted!");
            this.categoryDataServices.DeleteCategory(this.GetCategoryFromCategoryDto(category));
        }

        /// <summary>
        /// Gets the category by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="ServiceLayer.Utils.IncorrectIdException"></exception>
        public CategoryDTO GetCategoryById(int id)
        {
            this.log.Info("In GetCategoryById method");

            if (id < 0 || id == 0)
            {
                this.log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();
            }

            this.log.Info("The function GetCategoryById was successfully called.");
            return new CategoryDTO(this.categoryDataServices.GetCategoryById(id));
        }

        /// <summary>
        /// Gets the list of categories.
        /// </summary>
        /// <returns></returns>
        public IList<CategoryDTO> GetListOfCategories()
        {
            this.log.Info("In GetListOfCategories method");
            return this.categoryDataServices.GetListOfCategories().Select(c => new CategoryDTO(c)).ToList();
        }

        public void UpdateCategory(CategoryDTO category)
        {
            this.log.Info("In UpdateCategory method");
            this.ValidateCategory(category);

            var currentCategory = this.categoryDataServices.GetCategoryById(category.Id);
            if (currentCategory == null)
            {
                this.log.Warn("The ObjectNotFoundException was thrown!");
                throw new ObjectNotFoundException(category.Name);
            }

            this.log.Info("The function UpdateCategory was successfully called.");
            this.categoryDataServices.UpdateCategory(this.GetCategoryFromCategoryDto(category));
        }

        /// <summary>
        /// Gets the category from category dto.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        private Category GetCategoryFromCategoryDto(CategoryDTO category)
        {
            Category currentCategory = new Category
            {
                Id = category.Id,
                Name = category.Name,
            };

            return currentCategory;
        }

        /// <summary>
        /// Validates the category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <exception cref="ServiceLayer.Utils.InvalidObjectException"></exception>
        private void ValidateCategory(CategoryDTO category)
        {
            ValidationResults validationResults = Validation.Validate(category);
            if (validationResults.Count != 0)
            {
                throw new InvalidObjectException();
            }
        }
    }
}