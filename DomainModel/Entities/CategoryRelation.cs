// <copyright file="CategoryRelation.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DomainModel
{
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    public class CategoryRelation
    {
        /// <summary>
        /// Gets or sets the parent category.
        /// </summary>
        /// <value>
        /// The parent category.
        /// </value>
        [NotNullValidator]
        public Category ParentCategory { get; set; }

        /// <summary>
        /// Gets or sets the child category.
        /// </summary>
        /// <value>
        /// The child category.
        /// </value>
        [NotNullValidator]
        public Category ChildCategory { get; set; }
    }
}
