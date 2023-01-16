// <copyright file="OverbiddingException.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace ServiceLayer.Utils
{
    using System;

    public class OverbiddingException
    : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OverbiddingException"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public OverbiddingException(string name)
            : base(string.Format("Overbidding was detected for product: {0}", name))
        {
        }
    }
}
