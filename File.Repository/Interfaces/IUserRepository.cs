using Common.Layer.DTO;
using Common.Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace File.Repository.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserDTO> GetUsers();

        void AddUser(User user);
    }
}
