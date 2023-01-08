using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Utils
{
    public class InvalidObjectException: Exception
    {
        public InvalidObjectException()
           : base(String.Format("The object is not valid!"))
        {

        }
    }
}
