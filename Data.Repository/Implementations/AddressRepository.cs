using Data.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Layer.Extensions;
using Data.Repository.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Data.Repository.Implementations
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IConfiguration configuration;

        private readonly IMongoCollection<State> states;

        public AddressRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(this.configuration.GetConnectionString("MongoDb"));
            var database = client.GetDatabase("aem");
            this.states = database.GetCollection<State>("State");
        }

        public void AddState(State state)
        {
            state.Districts = new List<District>();
            this.states.InsertOne(state);
        }
        
        public IEnumerable<State> GetStates()
        {
            return this.states.Find(p => true).ToList();
        }

        public void AddDistrict(string stateId, District district)
        {
            var id = new ObjectId(stateId);
            district.Id = ObjectId.GenerateNewId();
            district.Taluks = new List<Taluk>();
            if (district.Feeders != null && district.Feeders.Any())
            {
                var feeder = district.Feeders.First();
                feeder.Id = ObjectId.GenerateNewId();
            }
            else
            {
                district.Feeders = new List<Feeder>();
            }

            this.states.UpdateOneAsync(
                Builders<State>.Filter.Eq(x => x.Id, id),
                Builders<State>.Update.Push(x => x.Districts, district));
        }

        public IEnumerable<District> GetDistricts(string stateId)
        {
            var id = new ObjectId(stateId);
            var state = this.states.Find(p => p.Id == id).FirstOrDefault();
            if(state != null)
            {
                return state.Districts;
            }

            return null;
        }

        public async Task AddTaluk(string stateId, string districtId, Taluk taluk)
        {
            var id = new ObjectId(stateId);
            taluk.Id = ObjectId.GenerateNewId();
            taluk.Villages = new List<Village>();
            if (taluk.Feeders != null && taluk.Feeders.Any())
            {
                var feeder = taluk.Feeders.First();
                feeder.Id = ObjectId.GenerateNewId();
                feeder.Transformers = new List<Transformer>();
            }
            else
            {
                taluk.Feeders = new List<Feeder>();
            }
            var did = new ObjectId(districtId);
            var filter = Builders<State>.Filter.And(
                Builders<State>.Filter.Where(x => x.Id == id),
                Builders<State>.Filter.Eq("Districts.Id", did));
            var update = Builders<State>.Update.Push("Districts.$.Taluks", taluk);
            await this.states.FindOneAndUpdateAsync(filter, update);
        }

        public async Task AddVillage(string stateId, string districtId, string talukId, Village village)
        {
            var id = new ObjectId(stateId);
            village.Id = ObjectId.GenerateNewId();
            if (village.Feeders != null && village.Feeders.Any())
            {
                var feeder = village.Feeders.First();
                feeder.Id = ObjectId.GenerateNewId();
                feeder.Transformers = new List<Transformer>();
            }
            else
            {
                village.Feeders = new List<Feeder>();
            }

            var did = new ObjectId(districtId);
            var tid = new ObjectId(talukId);
            
            var filter = Builders<State>.Filter.And(
                Builders<State>.Filter.Where(x => x.Id == id),
                Builders<State>.Filter.Eq("Districts.Id", did),
                Builders<State>.Filter.Eq("Districts.Taluks.Id", tid));
            var t = this.states.Find(filter).ToList();
            var y = new FindOneAndUpdateOptions<State, State>
            {
                ArrayFilters = new List<ArrayFilterDefinition>
                {
                    new BsonDocumentArrayFilterDefinition<State>(new BsonDocument("d._id", did)),
                    new BsonDocumentArrayFilterDefinition<State>(new BsonDocument("t._id", tid))
                }
            };
            //var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };
            var update = Builders<State>.Update.Push("Districts.$[d].Taluks.$[t].Villages", village);
            var t1 = await this.states.FindOneAndUpdateAsync(filter, update, y);
        }
    }
}
