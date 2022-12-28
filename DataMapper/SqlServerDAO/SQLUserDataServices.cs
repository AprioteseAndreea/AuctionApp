using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SqlServerDAO
{
    public class SQLUserDataServices : IUserDataServices
    {
        public void AddUser(User user)
        {
            using (var context = new MyApplicationContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void DeleteUser(User user)
        {
            using (var context = new MyApplicationContext())
            {
                var newUser = new User { Id = user.Id };
                context.Users.Attach(newUser);
                context.Users.Remove(newUser);
                context.SaveChanges();
            }
        }

        public IList<User> GetAllUsers()
        {
            using (var context = new MyApplicationContext())
            {
                return context.Users.Select(user => user).ToList();
            }
        }

        public User GetUserById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Users.Where(user => user.Id == id).SingleOrDefault();
            }
        }

        public void UpdateUser(User user)
        {
            using (var context = new MyApplicationContext())
            {
                var result = context.Users.First(u => u.Id == user.Id);
                if (result != null)
                {
                    result = user;
                    context.SaveChanges();
                }
            }
        }
    }
}
