using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
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
        [NotNullValidator]
        public Product Product
        {
            get;
            set;
        }

        [NotNullValidator]
        public User User
        {
            get;
            set;
        }

        [NotNullValidator]
        public Money Price
        {
            get;
            set;
        }
    }
}
