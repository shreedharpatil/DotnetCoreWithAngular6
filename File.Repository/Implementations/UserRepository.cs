using Common.Layer.Models;
using File.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace File.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly Configuration configuration;

        public UserRepository(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public void AddUser(User user)
        {
            var data = System.IO.File.ReadAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "User.json"));
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            if(users.Any())
            {
                users.Add(user);
            }
            else
            {
                users = new List<User> { user };
            }

            System.IO.File.WriteAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "User.json"), JsonConvert.SerializeObject(users));
        }

        public IEnumerable<User> GetUsers()
        {
            var data = System.IO.File.ReadAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "User.json"));
            return JsonConvert.DeserializeObject<List<User>>(data);

        }
    }
}
