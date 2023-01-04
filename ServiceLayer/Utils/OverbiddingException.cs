using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Utils
{
    public class OverbiddingException
    : Exception
    {
        public OverbiddingException() { }

        public OverbiddingException(string name)
            : base(String.Format("Overbidding was detected for product: {0}", name))
        {

        }
    }
}
