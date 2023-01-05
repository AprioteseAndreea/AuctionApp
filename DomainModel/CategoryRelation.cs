using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class CategoryRelation
    {
        public int Id
        {
            get;
            set;
        }
        [NotNullValidator]
        public Category ParentCategory
        {
            get;
            set;
        }

        [NotNullValidator]
        public Category ChildCategory
        {
            get;
            set;
        }
    }
}
