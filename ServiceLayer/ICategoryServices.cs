// <copyright file="ICategoryServices.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace ServiceLayer
{
    using System.Collections.Generic;
    using DomainModel.DTO;

    public interface ICategoryServices
    {
        /// <summary>
        /// Gets the list of categories.
        /// </summary>
        /// <returns></returns>
        IList<CategoryDTO> GetListOfCategories();

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="category">The category.</param>
        void DeleteCategory(CategoryDTO category);

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="category">The category.</param>
        void UpdateCategory(CategoryDTO category);

        /// <summary>
        /// Gets the category by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        CategoryDTO GetCategoryById(int id);

        /// <summary>
        /// Adds the category.
        /// </summary>
        /// <param name="category">The category.</param>
        void AddCategory(CategoryDTO category);
    }
}
