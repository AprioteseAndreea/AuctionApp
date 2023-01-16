// <copyright file="ICategoryRelationServices.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace ServiceLayer
{
    using System.Collections.Generic;
    using DomainModel.DTO;

    public interface ICategoryRelationServices
    {
        /// <summary>
        /// Gets the list of categories relation.
        /// </summary>
        /// <returns></returns>
        IList<CategoryRelationDTO> GetListOfCategoriesRelation();

        /// <summary>
        /// Deletes the category relation.
        /// </summary>
        /// <param name="category">The category.</param>
        void DeleteCategoryRelation(CategoryRelationDTO category);

        /// <summary>
        /// Gets the category relation by child and parent identifier.
        /// </summary>
        /// <param name="parentId">The parent identifier.</param>
        /// <param name="childId">The child identifier.</param>
        /// <returns></returns>
        CategoryRelationDTO GetCategoryRelationByChildAndParentId(int parentId, int childId);

        /// <summary>
        /// Updates the category relation.
        /// </summary>
        /// <param name="category">The category.</param>
        void UpdateCategoryRelation(CategoryRelationDTO category);

        /// <summary>
        /// Gets the category relation by parent identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IList<CategoryRelationDTO> GetCategoryRelationByParentId(int id);

        /// <summary>
        /// Gets the category relation by child identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IList<CategoryRelationDTO> GetCategoryRelationByChildId(int id);

        /// <summary>
        /// Adds the category relation.
        /// </summary>
        /// <param name="category">The category.</param>
        void AddCategoryRelation(CategoryRelationDTO category);
    }
}
