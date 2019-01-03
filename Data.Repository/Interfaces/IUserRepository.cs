using Common.Layer.DTO;
using Data.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        void AddUser(User user);
    }
}
