// <copyright file="IUserDataServices.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DataMapper
{
    using System.Collections.Generic;
    using DomainModel;

    public interface IUserDataServices
    {
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        IList<User> GetAllUsers();

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="user">The user.</param>
        void AddUser(User user);

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="user">The user.</param>
        void DeleteUser(User user);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        void UpdateUser(User user);

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        User GetUserById(int id);
    }
}
