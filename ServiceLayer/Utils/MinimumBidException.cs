// <copyright file="MinimumBidException.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace ServiceLayer.Utils
{
    using System;

    public class MinimumBidException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinimumBidException"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public MinimumBidException(string name)
            : base(string.Format("Minimum bid for product: {0}", name))
        {
        }
    }
}
