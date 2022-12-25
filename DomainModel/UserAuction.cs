using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class UserAuction
    {
        public int Id
        {
            get;
            set;
        }
        public int ProductId
        {
            get;
            set;
        }

        public int UserId
        {
            get;
            set;
        }

        public double Price
        {
            get;
            set;
        }
    }
}
