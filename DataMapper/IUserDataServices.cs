using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface IUserDataServices
    {
        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns></returns>
        IList<User> GetAllUsers();

        /// <summary>
        /// Adds the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        void AddUser(User user);

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        void DeleteUser(User user);

        /// <summary>
        /// Updates the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        void UpdateUser(User user);
        User GetUserById(int id);

    }
}
