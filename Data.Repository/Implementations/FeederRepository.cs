using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Layer.Extensions;
using Data.Repository.Interfaces;
using Data.Repository.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Data.Repository.Implementations
{
    public class FeederRepository : IFeederRepository
    {
        private readonly IConfiguration configuration;

        private readonly IMongoCollection<State> states;

        public FeederRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(this.configuration.GetConnectionString("MongoDb"));
            var database = client.GetDatabase("aem");
            this.states = database.GetCollection<State>("State");
        }

        public void AddFeeder(string type, Feeder feeder)
        {
            feeder.Transformers = new List<Transformer>();
            var states = this.states.Find(p => true).ToList();
            if (type.Equals("Village", StringComparison.InvariantCultureIgnoreCase))
            {
                var village = states.SelectMany(p => p.Districts.SelectMany(q => q.Taluks.SelectMany(r => r.Villages))).FirstOrDefault(p => p.Id == feeder.Id);
                if (village != null)
                {
                    var feeders = village.Feeders != null ? village.Feeders.ToList() : new List<Feeder>();
                    feeder.Id = ObjectId.GenerateNewId();
                    feeder.Transformers = new List<Transformer>();
                    feeders.Add(feeder);
                    village.Feeders = feeders;
                }
            }
            else if (type.Equals("Taluk", StringComparison.InvariantCultureIgnoreCase))
            {
                var taluk = states.SelectMany(p => p.Districts.SelectMany(q => q.Taluks)).FirstOrDefault(p => p.Id == feeder.Id);
                if (taluk != null)
                {
                    var feeders = taluk.Feeders != null ? taluk.Feeders.ToList() : new List<Feeder>();
                    feeder.Id = ObjectId.GenerateNewId();
                    feeder.Transformers = new List<Transformer>();
                    feeders.Add(feeder);
                    taluk.Feeders = feeders;
                }
            }
            else if (type.Equals("District", StringComparison.InvariantCultureIgnoreCase))
            {
                var district = states.SelectMany(p => p.Districts).FirstOrDefault(p => p.Id == feeder.Id);
                if (district != null)
                {
                    var feeders = district.Feeders != null ? district.Feeders.ToList() : new List<Feeder>();
                    feeder.Id = ObjectId.GenerateNewId();
                    feeder.Transformers = new List<Transformer>();
                    feeders.Add(feeder);
                    district.Feeders = feeders;
                }
            }
        }
    }
}
