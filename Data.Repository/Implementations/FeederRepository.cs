using Data.Repository.Interfaces;
using Data.Repository.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task AddFeeder(string type, string typeId, Feeder feeder)
        {
            feeder.Transformers = new List<Transformer>();
            feeder.Id = new ObjectId(typeId);
            if (type.Equals("Village", StringComparison.InvariantCultureIgnoreCase))
            {
                var filter = Builders<State>.Filter.And(
                    Builders<State>.Filter.Eq("Districts.Taluks.Villages.Id", feeder.Id));

                var t = this.states.Find(filter).FirstOrDefault();

                var data = t.Districts.Select(p =>
                {
                    var w = p.Taluks.FirstOrDefault(q => q.Villages.Any(r => r.Id == feeder.Id));
                    if (w != null)
                    {
                        return new { DistrictId = p.Id, TalukId = w.Id };
                    }

                    return null;
                }).FirstOrDefault(p => p != null);

                if (data != null)
                {
                    var y = new FindOneAndUpdateOptions<State, State>
                    {
                        ArrayFilters = new List<ArrayFilterDefinition>
                        {
                            new BsonDocumentArrayFilterDefinition<State>(new BsonDocument("d._id", data.DistrictId)),
                            new BsonDocumentArrayFilterDefinition<State>(new BsonDocument("t._id", data.TalukId)),
                            new BsonDocumentArrayFilterDefinition<State>(new BsonDocument("v._id", feeder.Id))
                        }
                    };
                    //var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };
                    var update = Builders<State>.Update.Push("Districts.$[d].Taluks.$[t].Villages.$[v].Feeders", feeder);
                    var t1 = await this.states.FindOneAndUpdateAsync(filter, update, y);
                }
            }
            else if (type.Equals("Taluk", StringComparison.InvariantCultureIgnoreCase))
            {
                var filter = Builders<State>.Filter.And(
                    Builders<State>.Filter.Eq("Districts.Taluks.Id", feeder.Id));

                var t = this.states.Find(filter).FirstOrDefault();

                var data = t.Districts.Select(p =>
                {
                    var w = p.Taluks.FirstOrDefault(r => r.Id == feeder.Id);
                    if (w != null)
                    {
                        return new {DistrictId = p.Id, TalukId = w.Id};
                    }

                    return null;
                }).FirstOrDefault(p => p != null);

                if (data != null)
                {
                    var y = new FindOneAndUpdateOptions<State, State>
                    {
                        ArrayFilters = new List<ArrayFilterDefinition>
                        {
                            new BsonDocumentArrayFilterDefinition<State>(new BsonDocument("d._id", data.DistrictId)),
                            new BsonDocumentArrayFilterDefinition<State>(new BsonDocument("t._id", data.TalukId))
                        }
                    };
                    //var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };
                    var update = Builders<State>.Update.Push("Districts.$[d].Taluks.$[t].Feeders", feeder);
                    var t1 = await this.states.FindOneAndUpdateAsync(filter, update, y);
                }
            }
            else if (type.Equals("District", StringComparison.InvariantCultureIgnoreCase))
            {
                var filter = Builders<State>.Filter.And(
                    Builders<State>.Filter.Eq("Districts.Id", feeder.Id));

                var t = this.states.Find(filter).FirstOrDefault();

                var y = new FindOneAndUpdateOptions<State, State>
                {
                    ArrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<State>(new BsonDocument("d._id", feeder.Id))
                    }
                };
                //var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };
                var update = Builders<State>.Update.Push("Districts.$[d].Feeders", feeder);
                var t1 = await this.states.FindOneAndUpdateAsync(filter, update, y);
            }
        }
    }
}
