using Common.Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace File.Repository.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        void AddUser(User user);
    }
}
