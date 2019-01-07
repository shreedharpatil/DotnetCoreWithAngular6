using Data.Repository.Interfaces;
using Data.Repository.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Data.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;

        private readonly IMongoCollection<User> users;

        private readonly IMongoCollection<State> states;

        public UserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(this.configuration.GetConnectionString("MongoDb"));
            var database = client.GetDatabase("aem");
            this.users = database.GetCollection<User>("User");
            this.states = database.GetCollection<State>("State");
        }

        public IEnumerable<User> GetUsers()
        {
            var usrs = users.Find(p => true).ToList();
            var fs = usrs.Where(p => !string.IsNullOrWhiteSpace(p.Feeder)).Select(p => new ObjectId(p.Feeder));
            var ts = usrs.Where(p => !string.IsNullOrWhiteSpace(p.Transformer)).Select(p => new ObjectId(p.Transformer));
            var ss = this.states.Find(p => true).ToList();
            var dfs = ss.SelectMany(p => p.Districts.SelectMany(q => q.Feeders)).ToList();
            var tfs = ss.SelectMany(p => p.Districts.SelectMany(q => q.Taluks.SelectMany(r => r.Feeders))).ToList();
            var ffs = ss.SelectMany(p => p.Districts.SelectMany(q => q.Taluks.SelectMany(r => r.Villages.SelectMany(s => s.Feeders)))).ToList();
            dfs.AddRange(tfs);
            dfs.AddRange(ffs);
            var dts = ss.SelectMany(p => p.Districts.SelectMany(q => q.Feeders.SelectMany(r => r.Transformers))).ToList();
            var dts1 = ss.SelectMany(p => p.Districts.SelectMany(q => q.Taluks.SelectMany(r => r.Feeders.SelectMany(s => s.Transformers)))).ToList();
            var dts2 = ss.SelectMany(p => p.Districts.SelectMany(q => q.Taluks.SelectMany(r => r.Villages.SelectMany(s => s.Feeders.SelectMany(o => o.Transformers))))).ToList();
            dts.AddRange(dts1);
            dts.AddRange(dts2);
            usrs.ForEach(p =>
            {
                var e = dfs.FirstOrDefault(p1 => fs.Contains(p1.Id));
                if (e != null)
                {
                    p.Feeder = $"{e.Name} -- {e.Description}";
                }
                var f = dts.FirstOrDefault(p1 => ts.Contains(p1.Id));
                if (f != null)
                {
                    p.Transformer = $"{f.Name} -- {f.Description}";
                }
            });

            return usrs;
        }

        public void AddUser(User user)
        {
            users.InsertOne(user);
        }
    }
}
