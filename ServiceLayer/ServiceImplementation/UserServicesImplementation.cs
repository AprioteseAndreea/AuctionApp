using DataMapper.SqlServerDAO;
using DataMapper;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImplementation
{
    public class UserServicesImplementation : IUserServices
    {
        IUserDataServices userDataServices = new SQLUserDataServices();

        public void AddUser(User user)
        {
           userDataServices.AddUser(user);
        }

        public void DeleteUser(User user)
        {
           userDataServices.DeleteUser(user);
        }

        public IList<User> GetListOfUsers()
        {
            return userDataServices.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return userDataServices.GetUserById(id);
        }

        public void UpdateUser(User user)
        {
            userDataServices.UpdateUser(user);
        }
    }
}
