using Common.Layer.DTO;
using Common.Layer.Models;
using File.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Layer.Extensions;

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

        

        public IEnumerable<UserDTO> GetUsers()
        {
            var data = System.IO.File.ReadAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "User.json"));
            var statesData = System.IO.File.ReadAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"));
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            var states = JsonConvert.DeserializeObject<List<State>>(statesData);
            var feeders = FeederExtensions.GetFeeders(states);
            var transformers = FeederExtensions.GetTransformers(states);

            var usrs = (from u in users
                       from f in feeders.Where(p => p.Id == u.Feeder).DefaultIfEmpty()
                       from t in transformers.Where(p => p.Id == u.Transformer).DefaultIfEmpty()
                        select new UserDTO
                        {
                            Address = u.Address,
                            District = u.District,
                            Email = u.Email,
                            EMMobile = u.EMMobile,
                            Feeder = f != null ? $"{f.Name} -- {f.Description}" : "No Feeder Assigned",
                            Transformer = t != null ? $"{t.Name} -- {t.Description}" : "No TC Assigned",
                            Firstname = u.Firstname,
                            Lastname = u.Lastname,
                            Mobile = u.Mobile,
                            Pincode = u.Pincode,
                            RRNo = u.RRNo,
                            State = u.State,
                            Taluk = u.Taluk,
                            Village = u.Village
                        }).ToList();

             return usrs;
        }
    }
}
