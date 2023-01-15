using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Utils
{
    public class ClosedAuctionException: Exception
    {
       
        public ClosedAuctionException(string product)
            : base(String.Format("The auction for product: {0} has been closed!", product))
        {

        }
    }
}
