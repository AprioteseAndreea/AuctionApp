// <copyright file="Money.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DomainModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.Enums;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    public class Money
    {
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        [NotNullValidator]
        [EnumDataType(typeof(Currency))]
        public Currency Currency { get; set; }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        /// <exception cref="System.Exception">nu se pot compara doua sume in valute diferite</exception>
        public static bool operator <(Money x, Money y)
        {
            if (x.Currency != y.Currency)
            {
                throw new Exception("nu se pot compara doua sume in valute diferite");
            }

            return x.Amount < y.Amount;
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        /// <exception cref="System.Exception">nu se pot compara doua sume in valute diferite</exception>
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
