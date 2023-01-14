using DomainModel;
using DomainModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
   public interface IUserServices
    {
        IList<UserDTO> GetListOfUsers();

        void DeleteUser(UserDTO user);

        void UpdateUser(UserDTO user);

        UserDTO GetUserById(int id);

        void AddUser(UserDTO user);
    }
}
