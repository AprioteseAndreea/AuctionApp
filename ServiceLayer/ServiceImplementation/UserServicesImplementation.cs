using DataMapper;
using DomainModel;
using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using ServiceLayer.Utils;
using log4net;

namespace ServiceLayer.ServiceImplementation
{
    public class UserServicesImplementation : IUserServices
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UserServicesImplementation));
        private readonly IUserDataServices userDataServices;
        private readonly IConfigurationDataServices configurationDataServices;

        public UserServicesImplementation(IUserDataServices userDataServices, IConfigurationDataServices configurationDataServices)
        {
            this.userDataServices = userDataServices;
            this.configurationDataServices = configurationDataServices;
        }

        public void AddUser(User user)
        {
            ValidationResults validationResults = Validation.Validate(user);
            if(validationResults.Count == 0)
            {
                var configuration = configurationDataServices.GetConfigurationById(1);
                if (configuration != null) user.Score = configuration.InitialScore;
                userDataServices.AddUser(user);
            }
            else
            {
                throw new InvalidObjectException();
            }
        }

        public void DeleteUser(User user)
        {
            log.Info("In DeleteUser method");

            if (user != null)
            {
                var currentUser = userDataServices.GetUserById(user.Id);
                if (currentUser != null)
                {
                    log.Info("The user have been deleted!");
                    userDataServices.DeleteUser(user);
                }
                else
                {
                    log.Warn("The user that you want to delete can not be found!");
                    throw new ObjectNotFoundException(user.Name);
                }
            }
            else
            {
                log.Warn("The object passed by parameter is null.");
                throw new NullReferenceException("The object can not be null.");
            }
        }

        public IList<User> GetListOfUsers()
        {
            log.Info("In GetListOfUsers method.");
            return userDataServices.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            log.Info("In GetUserById method");

            if (id < 0 || id == 0)
            {
                log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();

            }
            else
            {
                log.Info("The function GetUserById was successfully called.");
                return userDataServices.GetUserById(id);

            }
        }

        public void UpdateUser(User user)
        {
            log.Info("In UpdateUser method");
            ValidationResults validationResults = Validation.Validate(user);

            if (user != null && validationResults.Count == 0)
            {
                var currentUser = userDataServices.GetUserById(user.Id);
                if (currentUser != null)
                {
                    log.Info("The function UpdateUser was successfully called.");
                    userDataServices.UpdateUser(user);

                }
                else
                {
                    log.Warn("The ObjectNotFoundException was thrown!");
                    throw new ObjectNotFoundException(user.Name);
                }
            }
            else
            {
                log.Warn("The NullReferenceException was thrown!");
                throw new NullReferenceException("The object can not be null.");
            }
        }
    }
}
