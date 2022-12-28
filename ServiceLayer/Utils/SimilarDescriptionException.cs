using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Utils
{
     class SimilarDescriptionException: Exception
    {
        public SimilarDescriptionException() { }

        public SimilarDescriptionException(string name)
            : base(String.Format("Invalid Description for product: {0}", name))
        {

        }
    }

    
        
    
}
