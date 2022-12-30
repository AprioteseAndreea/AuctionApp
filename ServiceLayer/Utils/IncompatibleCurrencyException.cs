using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Utils
{
     class IncompatibleCurrencyException: Exception
    {
        public IncompatibleCurrencyException() { }

        public IncompatibleCurrencyException(string product)
            : base(String.Format("Incompatible currency between auction and product: {0}", product))
        {

        }
        
    }
}
