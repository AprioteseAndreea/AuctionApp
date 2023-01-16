// <copyright file="UserServicesImplementation.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace ServiceLayer.ServiceImplementation
{
    using System.Collections.Generic;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using DomainModel.DTO;
    using log4net;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using ServiceLayer.Utils;

    public class UserServicesImplementation : IUserServices
    {
        private readonly ILog log;
        private readonly IUserDataServices userDataServices;
        private readonly IConfigurationDataServices configurationDataServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserServicesImplementation"/> class.
        /// </summary>
        /// <param name="userDataServices">The user data services.</param>
        /// <param name="configurationDataServices">The configuration data services.</param>
        /// <param name="log">The log.</param>
        public UserServicesImplementation(
            IUserDataServices userDataServices,
            IConfigurationDataServices configurationDataServices,
            ILog log)
        {
            this.userDataServices = userDataServices;
            this.configurationDataServices = configurationDataServices;
            this.log = log;
        }

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void AddUser(UserDTO user)
        {
            this.ValidateUser(user);

            var configuration = this.configurationDataServices.GetConfigurationById(1);
            if (configuration != null)
            {
                user.Score = configuration.InitialScore;
            }

            this.userDataServices.AddUser(this.GetUserFromUserDto(user));
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="ServiceLayer.Utils.ObjectNotFoundException"></exception>
        public void DeleteUser(UserDTO user)
        {
            this.ValidateUser(user);

            var currentUser = this.userDataServices.GetUserById(user.Id);
            if (currentUser == null)
            {
                this.log.Warn("The user that you want to delete can not be found!");
                throw new ObjectNotFoundException(user.FirstName + user.LastName);
            }

            this.log.Info("The user have been deleted!");
            this.userDataServices.DeleteUser(this.GetUserFromUserDto(user));
        }

        /// <summary>
        /// Gets the list of users.
        /// </summary>
        /// <returns></returns>
        public IList<UserDTO> GetListOfUsers()
        {
            this.log.Info("In GetListOfUsers method.");
            return this.userDataServices.GetAllUsers().Select(u => new UserDTO(u)).ToList();
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="ServiceLayer.Utils.IncorrectIdException"></exception>
        public UserDTO GetUserById(int id)
        {
            this.log.Info("In GetUserById method");

            if (id < 0 || id == 0)
            {
                this.log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();
            }

            this.log.Info("The function GetUserById was successfully called.");
            return new UserDTO(this.userDataServices.GetUserById(id));
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="ServiceLayer.Utils.ObjectNotFoundException"></exception>
        public void UpdateUser(UserDTO user)
        {
            this.ValidateUser(user);

            var currentUser = this.userDataServices.GetUserById(user.Id);
            if (currentUser == null)
            {
                this.log.Warn("The ObjectNotFoundException was thrown!");
                throw new ObjectNotFoundException(user.FirstName + user.LastName);
            }

            this.log.Info("The function UpdateUser was successfully called.");
            this.userDataServices.UpdateUser(this.GetUserFromUserDto(user));
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="ServiceLayer.Utils.InvalidObjectException"></exception>
        private void ValidateUser(UserDTO user)
        {
            ValidationResults validationResults = Validation.Validate(user);
            if (validationResults.Count != 0)
            {
                throw new InvalidObjectException();
            }
        }

        /// <summary>
        /// Gets the user from user dto.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private User GetUserFromUserDto(UserDTO user)
        {
            User currentUser = new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Score = user.Score,
                Status = user.Status,
            };

            return currentUser;
        }
    }
}
