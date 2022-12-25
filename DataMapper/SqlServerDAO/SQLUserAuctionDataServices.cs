using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SqlServerDAO
{
    internal class SQLUserAuctionDataServices : IUserAuctionDataServices
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
                var newUser = new UserAuction { Id = userAuction.Id };
                context.UserAuctions.Attach(newUser);
                context.UserAuctions.Remove(newUser);
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
                return context.UserAuctions.Where(user => user.UserId == userId).ToList();
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
