using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Layer.Extensions;
using Data.Repository.Interfaces;
using Data.Repository.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Data.Repository.Implementations
{
    public class TransformerRepository : ITransformerRepository
    {
        private readonly IConfiguration configuration;

        private readonly IMongoCollection<State> states;

        public TransformerRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(this.configuration.GetConnectionString("MongoDb"));
            var database = client.GetDatabase("aem");
            this.states = database.GetCollection<State>("State");
        }

        public async Task AddTransformer(string type, string typeId, string feederId, Transformer transformer)
        {
            var fid = new ObjectId(feederId);
            var tid = new ObjectId(typeId);
            transformer.Id = ObjectId.GenerateNewId();
            if (type.Equals("Village", StringComparison.InvariantCultureIgnoreCase))
            {
                var filter = Builders<State>.Filter.And(
                    Builders<State>.Filter.Eq("Districts.Taluks.Villages.Id", tid));

                var t = this.states.Find(filter).FirstOrDefault();

                var data = t.Districts.Select(p =>
                {
                    var w = p.Taluks.FirstOrDefault(q => q.Villages.Any(r => r.Id == tid));
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
                            new BsonDocumentArrayFilterDefinition<State>(new BsonDocument("v._id", tid)),
                            new BsonDocumentArrayFilterDefinition<State>(new BsonDocument("f._id", fid))
                        }
                    };

                    var update =
                        Builders<State>.Update.Push(
                            "Districts.$[d].Taluks.$[t].Villages.$[v].Feeders.$[f].Transformers", transformer);
                    var t1 = await this.states.FindOneAndUpdateAsync(filter, update, y);
                }
            }
            else if (type.Equals("Taluk", StringComparison.InvariantCultureIgnoreCase))
            {
                var filter = Builders<State>.Filter.And(
                    Builders<State>.Filter.Eq("Districts.Taluks.Id", tid));

                var t = this.states.Find(filter).FirstOrDefault();

                var data = t.Districts.Select(p =>
                {
                    var w = p.Taluks.FirstOrDefault(r => r.Id == tid);
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
                            new BsonDocumentArrayFilterDefinition<State>(new BsonDocument("f._id", fid))
                        }
                    };
                    //var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };
                    var update = Builders<State>.Update.Push("Districts.$[d].Taluks.$[t].Feeders.$[f].Transformers", transformer);
                    var t1 = await this.states.FindOneAndUpdateAsync(filter, update, y);
                }
            }
            else if (type.Equals("District", StringComparison.InvariantCultureIgnoreCase))
            {
                var filter = Builders<State>.Filter.And(
                    Builders<State>.Filter.Eq("Districts.Id", tid));

                var t = this.states.Find(filter).FirstOrDefault();

                var y = new FindOneAndUpdateOptions<State, State>
                {
                    ArrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<State>(new BsonDocument("d._id", tid)),
                        new BsonDocumentArrayFilterDefinition<State>(new BsonDocument("f._id", fid))
                    }
                };
                //var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };
                var update = Builders<State>.Update.Push("Districts.$[d].Feeders.$[f].Transformers", transformer);
                var t1 = await this.states.FindOneAndUpdateAsync(filter, update, y);
            }
        }
    }
}
