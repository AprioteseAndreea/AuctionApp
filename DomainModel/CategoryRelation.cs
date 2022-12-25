using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class CategoryRelation
    {
        public int ParentCategory
        {
            get;
            set;
        }

        public int ChildCategory
        {
            get;
            set;
        }
    }
}
