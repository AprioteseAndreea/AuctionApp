using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Utils
{

    public class MaxAuctionsException : Exception
    {

        public MaxAuctionsException()
            : base(String.Format("The maximum number of licitations has been reached!"))
        {

        }

    }
}
