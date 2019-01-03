using Data.Repository.Interfaces;
using Data.Repository.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;

        private readonly IMongoCollection<User> users;

        public UserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(this.configuration.GetConnectionString("MongoDb"));
            var database = client.GetDatabase("aem");
            this.users = database.GetCollection<User>("User");
        }

        public IEnumerable<User> GetUsers()
        {
            return users.Find(p => true).ToList();
        }

        public void AddUser(User user)
        {
            users.InsertOne(user);
        }
    }
}
