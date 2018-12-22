using Common.Layer.DTO;
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

        public IEnumerable<UserDTO> GetUsers()
        {
            var data = System.IO.File.ReadAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "User.json"));
            var statesData = System.IO.File.ReadAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"));
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            var states = JsonConvert.DeserializeObject<List<State>>(statesData);
            var districts = states.SelectMany(p => p.Districts.SelectMany(q => q.Feeders)).ToList();
            var taluks = states.SelectMany(p => p.Districts.SelectMany(q => q.Taluks.SelectMany(r => r.Feeders))).ToList();
            var villages = states.SelectMany(p => p.Districts.SelectMany(q => q.Taluks.SelectMany(r => r.Villages.SelectMany(s => s.Feeders)))).ToList();
            districts.AddRange(taluks);
            districts.AddRange(villages);
            var usrs = (from u in users
                       from f in districts.Where(p => p.Id == u.Feeder).DefaultIfEmpty()
                        select new UserDTO
                        {
                            Address = u.Address,
                            District = u.District,
                            Email = u.Email,
                            EMMobile = u.EMMobile,
                            Feeder = f != null ? $"{f.Name} -- {f.Description}" : "No Feeder Assigned",
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
