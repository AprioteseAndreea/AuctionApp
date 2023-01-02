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
        public int Product
        {
            get;
            set;
        }
        
        public int User
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

        [DomainValidator("Opened", "Closed", MessageTemplate = "Unknown status")]
        public string Status
        {
            get;
            set;
        }
    }
}
