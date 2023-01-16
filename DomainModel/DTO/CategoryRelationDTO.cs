// <copyright file="CategoryRelationDTO.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DomainModel.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryRelationDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRelationDTO"/> class.
        /// </summary>
        public CategoryRelationDTO()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRelationDTO"/> class.
        /// </summary>
        /// <param name="categoryRelation">The category relation.</param>
        public CategoryRelationDTO(CategoryRelation categoryRelation)
        {
            this.ParentCategoryId = categoryRelation.ParentCategory.Id;
            this.ChildCategoryId = categoryRelation.ChildCategory.Id;
        }

        /// <summary>
        /// Gets or sets the parent category identifier.
        /// </summary>
        /// <value>
        /// The parent category identifier.
        /// </value>
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int ParentCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the child category identifier.
        /// </summary>
        /// <value>
        /// The child category identifier.
        /// </value>
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int ChildCategoryId { get; set; }
    }
}
