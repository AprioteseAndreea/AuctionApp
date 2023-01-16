namespace DataMapper.SqlServerDAO
{
    using System.Collections.Generic;
    using System.Linq;
    using DomainModel;

    public class SQLUserAuctionDataServices : IUserAuctionDataServices
    {
        public void AddUserAuction(UserAuction userAuction)
        {
            using (var context = new MyApplicationContext())
            {
                context.UserAuctions.Add(userAuction);
                context.SaveChanges();
            }
        }

        public void DeleteUserAuction(UserAuction userAuction)
        {
            using (var context = new MyApplicationContext())
            {
                context.UserAuctions.Attach(userAuction);
                context.UserAuctions.Remove(userAuction);
                context.SaveChanges();
            }
        }

        public IList<UserAuction> GetListOfUserAuctions()
        {
            using (var context = new MyApplicationContext())
            {
                return context.UserAuctions.Select(user => user).ToList();
            }
        }

        public UserAuction GetUserAuctionById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.UserAuctions.Where(user => user.Id == id).SingleOrDefault();
            }
        }

        public IList<UserAuction> GetUserAuctionsByUserId(int userId)
        {
            using (var context = new MyApplicationContext())
            {
                return context.UserAuctions.Where(user => user.User.Id == userId).ToList();
            }
        }

        public IList<UserAuction> GetUserAuctionsByUserIdandProductId(int userId, int productId)
        {
            using (var context = new MyApplicationContext())
            {
                return context.UserAuctions.Where(user => user.User.Id == userId && user.Product.Id == productId).ToList();
            }
        }

        public void UpdateUserAuction(UserAuction userAuction)
        {
            using (var context = new MyApplicationContext())
            {
                var result = context.UserAuctions.First(u => u.Id == userAuction.Id);
                if (result != null)
                {
                    result = userAuction;
                    context.SaveChanges();
                }
            }
        }
    }
}