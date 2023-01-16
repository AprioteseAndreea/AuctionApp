namespace ServiceLayer.Utils
{
    using System;

    public class ClosedAuctionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClosedAuctionException"/> class.
        /// </summary>
        /// <param name="product">The product.</param>
        public ClosedAuctionException(string product)
            : base(string.Format("The auction for product: {0} has been closed!", product))
        {
        }
    }
}
