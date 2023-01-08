using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Utils
{

    public class IncorrectIdException : Exception
    {
        public IncorrectIdException()
                        : base(String.Format("The id has an incorrect format"))
        {

        }

    }
}
