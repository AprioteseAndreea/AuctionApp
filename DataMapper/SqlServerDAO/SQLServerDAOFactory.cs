using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SqlServerDAO
{
    class SQLServerDAOFactory : IDAOFactory
    {
        public IUserDataServices UserDataServices
        {
            get
            {
                return new SQLUserDataServices();
            }
        }

        public IProductDataServices ProducDataServices
        {
            get
            {
                return new SQLProductDataServices();
            }
        }

        public IUserAuctionDataServices UserAuctionDataServices => throw new NotImplementedException();

        public ICategoryDataServices CategoryDataServices => throw new NotImplementedException();

        public ICategoryRelationDataServices CategoryRelationDataServices => throw new NotImplementedException();
    }
}
