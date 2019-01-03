using Data.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var state = this.states.Find(p => p.Id == id).FirstOrDefault();
            district.Id = ObjectId.GenerateNewId();
            if (state.Districts == null)
            {
                state.Districts = new List<District>();
            }

            district.Taluks = new List<Taluk>();
            if(district.Feeders != null && district.Feeders.Any())
            {
                var feeder = district.Feeders.First();
                feeder.Id = ObjectId.GenerateNewId();
            }
            else
            {
                district.Feeders = new List<Feeder>();
            }

            state.Districts.Add(district);
            
            this.states.ReplaceOne(p => p.Id == id, state);
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

        public void AddTaluk(string stateId, string districtId, Taluk taluk)
        {
            var id = new ObjectId(stateId);
            var state = this.states.Find(p => p.Id == id).FirstOrDefault();
            if (state != null)
            {
                var district = state.Districts.FirstOrDefault(p => p.Id == new ObjectId(districtId));
                if(district != null)
                {
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

                    if(district.Taluks == null)
                    {
                        district.Taluks = new List<Taluk>();
                    }

                    district.Taluks.Add(taluk);
                }
            }

            this.states.ReplaceOne(p => p.Id == id, state);
        }

        public void AddVillage(string stateId, string districtId, string talukId, Village village)
        {
            var id = new ObjectId(stateId);
            var state = this.states.Find(p => p.Id == id).FirstOrDefault();
            if (state != null)
            {
                var district = state.Districts.FirstOrDefault(p => p.Id == new ObjectId(districtId));
                if (district != null)
                {
                    var taluk = district.Taluks.FirstOrDefault(p => p.Id == new ObjectId(talukId));
                    if(taluk != null)
                    {
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

                        if (taluk.Villages == null)
                        {
                            taluk.Villages = new List<Village>();
                        }

                        taluk.Villages.Add(village);
                    }
                }
            }

            this.states.ReplaceOne(p => p.Id == id, state);
        }
    }
}
