using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Utils
{
    public class ObjectNotFoundException: Exception
    {     
        public ObjectNotFoundException(string name)
                        : base(String.Format("Product: {0} was noit found!", name))
        {

        }

    }
}
