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
            throw new NotImplementedException();
        }

        public IList<User> GetListOfUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
