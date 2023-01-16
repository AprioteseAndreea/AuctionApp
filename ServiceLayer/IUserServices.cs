namespace ServiceLayer
{
    using System.Collections.Generic;
    using DomainModel.DTO;

    public interface IUserServices
    {
        /// <summary>
        /// Gets the list of users.
        /// </summary>
        /// <returns></returns>
        IList<UserDTO> GetListOfUsers();

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="user">The user.</param>
        void DeleteUser(UserDTO user);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        void UpdateUser(UserDTO user);

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        UserDTO GetUserById(int id);

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="user">The user.</param>
        void AddUser(UserDTO user);
    }
}
