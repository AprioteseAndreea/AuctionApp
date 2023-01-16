namespace DataMapper
{
    using System.Collections.Generic;
    using DomainModel;

    public interface IUserDataServices
    {
        IList<User> GetAllUsers();

        void AddUser(User user);

        void DeleteUser(User user);

        void UpdateUser(User user);

        User GetUserById(int id);
    }
}
