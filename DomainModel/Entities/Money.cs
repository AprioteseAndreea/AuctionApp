namespace DomainModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.Enums;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    public class Money
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal Amount { get; set; }

        [NotNullValidator]
        [EnumDataType(typeof(Currency))]
        public Currency Currency { get; set; }

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
    }
}
