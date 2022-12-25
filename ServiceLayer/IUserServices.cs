using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    interface IUserServices
    {
        IList<User> GetListOfUsers();

        void DeleteUser(User user);

        void UpdateUser(User user);

        User GetUserById(int id);

        void AddUser(User user);
    }
}
