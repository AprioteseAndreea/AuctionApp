using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Money
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal Amount
        {
            get;
            set;
        }

        [NotNullValidator]
        [DomainValidator("RON", "USD", "EUR", MessageTemplate = "The currency {0} is not allowed")]
        public String Currency
        {
            get;
            set;
        }

        public static bool operator <(Money x, Money y)
        {
            if (x.Currency != y.Currency)
            {
                throw new Exception("nu se pot compara doua sume in valute diferite");
            }
            return x.Amount < y.Amount;
        }

        public static bool operator >(Money x, Money y)
        {
            if (x.Currency != y.Currency)
            {
                throw new Exception("nu se pot compara doua sume in valute diferite");
            }
            return x.Amount > y.Amount;
        }

        public override string ToString()
        {
            return Amount.ToString() + " " + Currency.ToString();
        }
    }
}
