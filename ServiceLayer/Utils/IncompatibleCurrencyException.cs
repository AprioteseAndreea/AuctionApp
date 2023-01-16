// <copyright file="IncompatibleCurrencyException.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>
namespace ServiceLayer.Utils
{
    using System;

    public class IncompatibleCurrencyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IncompatibleCurrencyException"/> class.
        /// </summary>
        /// <param name="product">The product.</param>
        public IncompatibleCurrencyException(string product)
            : base(string.Format("Incompatible currency between auction and product: {0}", product))
        {
        }
    }
}
