// <copyright file="CategoryDTO.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DomainModel.DTO
{
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    public class CategoryDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryDTO"/> class.
        /// </summary>
        public CategoryDTO()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryDTO"/> class.
        /// </summary>
        /// <param name="category">The category.</param>
        public CategoryDTO(Category category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [NotNullValidator(MessageTemplate = "The name cannot be null")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The name should have between {3} and {5} letters")]
        public string Name { get; set; }
    }
}