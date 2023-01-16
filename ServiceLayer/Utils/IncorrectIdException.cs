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
