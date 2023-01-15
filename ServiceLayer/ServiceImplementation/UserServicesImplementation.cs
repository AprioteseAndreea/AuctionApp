using DataMapper;
using DomainModel;
using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using ServiceLayer.Utils;
using log4net;
using DomainModel.DTO;
using System.Linq;

namespace ServiceLayer.ServiceImplementation
{
    public class UserServicesImplementation : IUserServices
    {
        private readonly ILog log;
        private readonly IUserDataServices userDataServices;
        private readonly IConfigurationDataServices configurationDataServices;

        public UserServicesImplementation(
            IUserDataServices userDataServices,
            IConfigurationDataServices configurationDataServices,
            ILog log)
        {
            this.userDataServices = userDataServices;
            this.configurationDataServices = configurationDataServices;
            this.log = log;
        }

        public void AddUser(UserDTO user)
        {
            ValidateUser(user);

            var configuration = configurationDataServices.GetConfigurationById(1);
            if (configuration != null) user.Score = configuration.InitialScore;

            userDataServices.AddUser(GetUserFromUserDto(user));
        }

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
        private void ValidateUser(UserDTO user)
        {
            ValidationResults validationResults = Validation.Validate(user);
            if (validationResults.Count != 0) throw new InvalidObjectException();
        }
        public void DeleteUser(UserDTO user)
        {
            ValidateUser(user);

            var currentUser = userDataServices.GetUserById(user.Id);
            if (currentUser == null)
            {
                log.Warn("The user that you want to delete can not be found!");
                throw new ObjectNotFoundException(user.FirstName + user.LastName);

            }
            log.Info("The user have been deleted!");
            userDataServices.DeleteUser(GetUserFromUserDto(user));
        }

        public IList<UserDTO> GetListOfUsers()
        {
            log.Info("In GetListOfUsers method.");
            return userDataServices.GetAllUsers().Select(u => new UserDTO(u)).ToList();
        }

        public UserDTO GetUserById(int id)
        {
            log.Info("In GetUserById method");

            if (id < 0 || id == 0)
            {
                log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();

            }

            log.Info("The function GetUserById was successfully called.");
            return new UserDTO(userDataServices.GetUserById(id));
        }

        public void UpdateUser(UserDTO user)
        {
            ValidateUser(user);

            var currentUser = userDataServices.GetUserById(user.Id);
            if (currentUser == null)
            {
                log.Warn("The ObjectNotFoundException was thrown!");
                throw new ObjectNotFoundException(user.FirstName + user.LastName);
            }

            log.Info("The function UpdateUser was successfully called.");
            userDataServices.UpdateUser(GetUserFromUserDto(user));
        }
    }
}
