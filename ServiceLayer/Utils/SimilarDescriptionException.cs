namespace ServiceLayer.Utils
{
    using System;

    public class SimilarDescriptionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimilarDescriptionException"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public SimilarDescriptionException(string name)
            : base(string.Format("Invalid Description for product: {0}", name))
        {
        }
    }
}
