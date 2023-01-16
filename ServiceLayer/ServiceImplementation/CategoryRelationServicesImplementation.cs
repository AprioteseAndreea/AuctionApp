// <copyright file="CategoryRelationServicesImplementation.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

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

    public class CategoryRelationServicesImplementation : ICategoryRelationServices
    {
        private readonly ICategoryRelationDataServices categoryRelationServices;
        private readonly ICategoryDataServices categoryDataServices;
        private readonly ILog log;

        /// <summary>Initializes a new instance of the <see cref="CategoryRelationServicesImplementation" /> class.</summary>
        /// <param name="categoryRelationServices">The category relation services.</param>
        /// <param name="categoryDataServices">The category data services.</param>
        /// <param name="log">The log.</param>
        public CategoryRelationServicesImplementation(
            ICategoryRelationDataServices categoryRelationServices,
            ICategoryDataServices categoryDataServices,
            ILog log)
        {
            this.categoryRelationServices = categoryRelationServices;
            this.categoryDataServices = categoryDataServices;
            this.log = log;
        }

        /// <summary>Adds the category relation.</summary>
        /// <param name="category">The category.</param>
        public void AddCategoryRelation(CategoryRelationDTO category)
        {
            this.ValidateCategoryRelation(category);
            this.CheckIfChildAndParentCategoryExist(category);

            this.categoryRelationServices.AddCategoryRelation(this.GetCategoryFromCategoryDto(category));
        }

        /// <summary>Deletes the category relation.</summary>
        /// <param name="category">The category.</param>
        /// <exception cref="ServiceLayer.Utils.ObjectNotFoundException"></exception>
        public void DeleteCategoryRelation(CategoryRelationDTO category)
        {
            this.log.Info("In DeleteCategoryRelation method");

            this.ValidateCategoryRelation(category);

            var currentCategory = this.categoryRelationServices
                .GetCategoryRelationByChildAndParentId(category.ChildCategoryId, category.ParentCategoryId);

            if (currentCategory == null)
            {
                this.log.Warn("The category relation that you want to delete can not be found!");
                throw new ObjectNotFoundException(string.Empty);
            }

            this.log.Info("The category relation have been deleted!");
            this.categoryRelationServices.DeleteCategoryRelation(this.GetCategoryFromCategoryDto(category));
        }

        /// <summary>Gets the category relation by parent identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="ServiceLayer.Utils.IncorrectIdException"></exception>
        public IList<CategoryRelationDTO> GetCategoryRelationByParentId(int id)
        {
            this.log.Info("In GetCategoryRelationByParentId method");

            if (id < 0 || id == 0)
            {
                this.log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();
            }

            this.log.Info("The function GetCategoryRelationByParentId was successfully called.");
            return this.categoryRelationServices.GetCategoryRelationByParentId(id)
                .Select(c => new CategoryRelationDTO(c)).ToList();
        }

        /// <summary>Gets the list of categories relation.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public IList<CategoryRelationDTO> GetListOfCategoriesRelation()
        {
            this.log.Info("In GetListOfCategoriesRelation method");
            return this.categoryRelationServices.GetListOfCategoriesRelation()
                .Select(c => new CategoryRelationDTO(c)).ToList();
        }

        /// <summary>Updates the category relation.</summary>
        /// <param name="category">The category.</param>
        /// <exception cref="ServiceLayer.Utils.ObjectNotFoundException"></exception>
        public void UpdateCategoryRelation(CategoryRelationDTO category)
        {
            this.log.Info("In UpdateCategoryRelation method");

            this.ValidateCategoryRelation(category);

            var currentCategory = this.categoryRelationServices
                .GetCategoryRelationByChildAndParentId(category.ChildCategoryId, category.ParentCategoryId);
            if (currentCategory == null)
            {
                this.log.Warn("The ObjectNotFoundException was thrown!");
                throw new ObjectNotFoundException(string.Empty);
            }

            this.log.Info("The function UpdateCategoryRelation was successfully called.");
            this.categoryRelationServices.UpdateCategoryRelation(this.GetCategoryFromCategoryDto(category));
        }

        /// <summary>Gets the category relation by child and parent identifier.</summary>
        /// <param name="parentId">The parent identifier.</param>
        /// <param name="childId">The child identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="ServiceLayer.Utils.IncorrectIdException"></exception>
        public CategoryRelationDTO GetCategoryRelationByChildAndParentId(int parentId, int childId)
        {
            this.log.Info("In GetCategoryRelationById method");

            if (parentId < 0 || childId == 0)
            {
                this.log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();
            }

            this.log.Info("The function GetCategoryById was successfully called.");
            return new CategoryRelationDTO(this.categoryRelationServices.GetCategoryRelationByChildAndParentId(parentId, childId));
        }

        /// <summary>Gets the category relation by child identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="ServiceLayer.Utils.IncorrectIdException"></exception>
        public IList<CategoryRelationDTO> GetCategoryRelationByChildId(int id)
        {
            this.log.Info("In GetCategoryRelationByChildId method");

            if (id < 0 || id == 0)
            {
                this.log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();
            }

            this.log.Info("The function GetCategoryRelationByChildId was successfully called.");
            return this.categoryRelationServices
                .GetCategoryRelationByChildId(id).Select(c => new CategoryRelationDTO(c)).ToList();
        }

        /// <summary>Validates the category relation.</summary>
        /// <param name="category">The category.</param>
        /// <exception cref="ServiceLayer.Utils.InvalidObjectException"></exception>
        private void ValidateCategoryRelation(CategoryRelationDTO category)
        {
            ValidationResults validationResults = Validation.Validate(category);
            if (validationResults.Count != 0)
            {
                throw new InvalidObjectException();
            }
        }

        /// <summary>Checks if child and parent category exist.</summary>
        /// <param name="category">The category.</param>
        /// <exception cref="ServiceLayer.Utils.ObjectNotFoundException"></exception>
        private void CheckIfChildAndParentCategoryExist(CategoryRelationDTO category)
        {
            var childCategory = this.categoryDataServices.GetCategoryById(category.ChildCategoryId);
            var parentCategory = this.categoryDataServices.GetCategoryById(category.ParentCategoryId);

            if (childCategory == null || parentCategory == null)
            {
                throw new ObjectNotFoundException(string.Empty);
            }
        }

        /// <summary>Gets the category from category dto.</summary>
        /// <param name="category">The category.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private CategoryRelation GetCategoryFromCategoryDto(CategoryRelationDTO category)
        {
            var childCategory = this.categoryDataServices.GetCategoryById(category.ChildCategoryId);
            var parentCategory = this.categoryDataServices.GetCategoryById(category.ParentCategoryId);

            CategoryRelation currentCategory = new CategoryRelation
            {
                ParentCategory = parentCategory,
                ChildCategory = childCategory,
            };

            return currentCategory;
        }
    }
}
