using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Utils
{
     public class IncompatibleCurrencyException: Exception
    {
        
        public IncompatibleCurrencyException(string product)
            : base(String.Format("Incompatible currency between auction and product: {0}", product))
        {

        }
        
    }
}
