// <copyright file="ICategoryDataServices.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DataMapper
{
    using System.Collections.Generic;
    using DomainModel;

    public interface ICategoryDataServices
    {
        /// <summary>
        /// Gets the list of categories.
        /// </summary>
        /// <returns></returns>
        IList<Category> GetListOfCategories();

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="category">The category.</param>
        void DeleteCategory(Category category);

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="category">The category.</param>
        void UpdateCategory(Category category);

        /// <summary>
        /// Gets the category by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Category GetCategoryById(int id);

        /// <summary>
        /// Adds the category.
        /// </summary>
        /// <param name="category">The category.</param>
        void AddCategory(Category category);
    }
}
