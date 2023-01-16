﻿namespace DomainModel
{
    using System.Collections.Generic;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    public class Category
    {
        public Category()
        {
        }

        public int Id { get; set; }

        [NotNullValidator(MessageTemplate = "The name cannot be null")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The name should have between {3} and {5} letters")]
        public string Name { get; set; }

        [NotNullValidator]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
