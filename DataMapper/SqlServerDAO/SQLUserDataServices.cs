// <copyright file="SQLUserDataServices.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System.Collections.Generic;
    using System.Linq;
    using DomainModel;

    public class SQLUserDataServices : IUserDataServices
    {
        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void AddUser(User user)
        {
            using (var context = new MyApplicationContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="user">The user.</param>
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

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public IList<User> GetAllUsers()
        {
            using (var context = new MyApplicationContext())
            {
                return context.Users.Select(user => user).ToList();
            }
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public User GetUserById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Users.Where(user => user.Id == id).SingleOrDefault();
            }
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void UpdateUser(User user)
        {
            using (var context = new MyApplicationContext())
            {
                var result = context.Users.First(u => u.Id == user.Id);
                if (result != null)
                {
                    result.FirstName = user.FirstName;
                    result.LastName = user.LastName;
                    result.Status = user.Status;
                    context.SaveChanges();
                }
            }
        }
    }
}