using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }
        public int Id
        {
            get;
            set;
        }
        [NotNullValidator(MessageTemplate = "The name cannot be null")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The name should have between {3} and {5} letters")]
        public string Name
        {
            get;
            set;
        }

        public virtual ICollection<Product> Products
        {
            get;
            set;
        }
    }
}
