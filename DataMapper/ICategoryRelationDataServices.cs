// <copyright file="ICategoryRelationDataServices.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DataMapper
{
    using System.Collections.Generic;
    using DomainModel;

    public interface ICategoryRelationDataServices
    {
        /// <summary>
        /// Gets the list of categories relation.
        /// </summary>
        /// <returns></returns>
        IList<CategoryRelation> GetListOfCategoriesRelation();

        /// <summary>
        /// Deletes the category relation.
        /// </summary>
        /// <param name="category">The category.</param>
        void DeleteCategoryRelation(CategoryRelation category);

        /// <summary>
        /// Gets the category relation by child and parent identifier.
        /// </summary>
        /// <param name="parentId">The parent identifier.</param>
        /// <param name="childId">The child identifier.</param>
        /// <returns></returns>
        CategoryRelation GetCategoryRelationByChildAndParentId(int parentId, int childId);

        /// <summary>
        /// Updates the category relation.
        /// </summary>
        /// <param name="category">The category.</param>
        void UpdateCategoryRelation(CategoryRelation category);

        /// <summary>
        /// Gets the category relation by parent identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IList<CategoryRelation> GetCategoryRelationByParentId(int id);

        /// <summary>
        /// Gets the category relation by child identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IList<CategoryRelation> GetCategoryRelationByChildId(int id);

        /// <summary>
        /// Adds the category relation.
        /// </summary>
        /// <param name="category">The category.</param>
        void AddCategoryRelation(CategoryRelation category);
    }
}