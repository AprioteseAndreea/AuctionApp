// <copyright file="MaxAuctionsException.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace ServiceLayer.Utils
{
    using System;

    public class MaxAuctionsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaxAuctionsException"/> class.
        /// </summary>
        public MaxAuctionsException()
            : base(string.Format("The maximum number of licitations has been reached!"))
        {
        }
    }
}
