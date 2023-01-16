// <copyright file="IncorrectIdException.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace ServiceLayer.Utils
{
    using System;

    public class IncorrectIdException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IncorrectIdException"/> class.
        /// </summary>
        public IncorrectIdException()
                        : base(string.Format("The id has an incorrect format"))
        {
        }
    }
}
