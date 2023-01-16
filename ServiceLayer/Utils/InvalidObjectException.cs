namespace ServiceLayer.Utils
{
    using System;

    public class InvalidObjectException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidObjectException"/> class.
        /// </summary>
        public InvalidObjectException()
           : base(string.Format("The object is not valid!"))
        {
        }
    }
}
