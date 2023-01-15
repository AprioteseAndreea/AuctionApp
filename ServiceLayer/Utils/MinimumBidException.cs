using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Utils
{
    public class MinimumBidException : Exception
    {
     
        public MinimumBidException(string name)
            : base(String.Format("Minimum bid for product: {0}", name))
        {

        }
    }

}
