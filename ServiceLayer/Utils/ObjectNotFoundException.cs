// <copyright file="ObjectNotFoundException.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace ServiceLayer.Utils
{
    using System;

    public class ObjectNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNotFoundException"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ObjectNotFoundException(string name)
                        : base(string.Format("Product: {0} was noit found!", name))
        {
        }
    }
}
