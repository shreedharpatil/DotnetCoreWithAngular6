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
    public class AddressRepository : IAddressRepository
    {
        private readonly Configuration configuration;

        public AddressRepository(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public void AddState(State state)
        {
            var data = System.IO.File.ReadAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"));
            var states = JsonConvert.DeserializeObject<List<State>>(data);
            state.Districts = new List<District>();
            if (states.Any())
            {
                state.Id = states.Max(p => p.Id) + 1;                
                states.Add(state);
            }
            else
            {
                state.Id = 1;
                states = new List<State> { state };
            }

            System.IO.File.WriteAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"), JsonConvert.SerializeObject(states));
        }
        
        public IEnumerable<State> GetStates()
        {
            var data = System.IO.File.ReadAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"));
            return JsonConvert.DeserializeObject<List<State>>(data);
        }

        public void AddDistrict(int stateId, District district)
        {
            var data = System.IO.File.ReadAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"));
            var states = JsonConvert.DeserializeObject<List<State>>(data);
            
            if (states.Any())
            {
                district.Taluks = new List<Taluk>();
                var ds = states.SelectMany(p => p.Districts);
                var t = ds.Any() ? ds.Max(p => p.Id) : 0;
                district.Id = t + 1;

                if (district.Feeders != null && district.Feeders.Any())
                {
                    var feeder = district.Feeders.First();
                    feeder.Id = FeederExtensions.GetNextFeederId(states);
                    district.Feeders = new List<Feeder> { feeder };
                }
                else
                {
                    district.Feeders = new List<Feeder>();
                }

                var state = states.FirstOrDefault(p => p.Id == stateId);
                var districts = state.Districts != null ? state.Districts.ToList() : new List<District>();
                districts.Add(district);

                state.Districts = districts;
            }

            System.IO.File.WriteAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"), JsonConvert.SerializeObject(states));
        }

        public IEnumerable<District> GetDistricts(int stateId)
        {
            var data = System.IO.File.ReadAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"));
            var states = JsonConvert.DeserializeObject<List<State>>(data);
            var state = states.FirstOrDefault(p => p.Id == stateId);
            return state?.Districts;
        }

        public void AddTaluk(int stateId, int districtId, Taluk taluk)
        {
            var data = System.IO.File.ReadAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"));
            var states = JsonConvert.DeserializeObject<List<State>>(data);

            if (states.Any())
            {
                var state = states.FirstOrDefault(p => p.Id == stateId);
                var district = state.Districts.FirstOrDefault(p => p.Id == districtId);
                if(district != null)
                {
                    taluk.Villages = new List<Village>();
                    var tqs = states.SelectMany(p => p.Districts.SelectMany(q => q.Taluks));
                    var t = tqs.Any() ? tqs.Max(p => p.Id) : 0;
                    taluk.Id = t + 1;

                    if(taluk.Feeders != null && taluk.Feeders.Any())
                    {                        
                        var feeder = taluk.Feeders.First();
                        feeder.Id = FeederExtensions.GetNextFeederId(states);
                        taluk.Feeders = new List<Feeder> { feeder };
                    }
                    else
                    {
                        taluk.Feeders = new List<Feeder>();
                    }
                    
                    var taluks = district.Taluks != null ? district.Taluks.ToList() : new List<Taluk>();
                    taluks.Add(taluk);
                    district.Taluks = taluks;
                }                
            }

            System.IO.File.WriteAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"), JsonConvert.SerializeObject(states));
        }

        public void AddVillage(int stateId, int districtId, int talukId, Village village)
        {
            var data = System.IO.File.ReadAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"));
            var states = JsonConvert.DeserializeObject<List<State>>(data);

            if (states.Any())
            {
                var state = states.FirstOrDefault(p => p.Id == stateId);
                var district = state.Districts.FirstOrDefault(p => p.Id == districtId);
                var taluk = district.Taluks.FirstOrDefault(p => p.Id == talukId);
                
                if (taluk != null)
                {
                    var vls = states.SelectMany(p => p.Districts.SelectMany(q => q.Taluks.SelectMany(r => r.Villages)));
                    var t = vls.Any() ? vls.Max(p => p.Id) : 0;
                    village.Id = t + 1;

                    if (village.Feeders != null && village.Feeders.Any())
                    {
                        var feeder = village.Feeders.First();
                        feeder.Id = FeederExtensions.GetNextFeederId(states);
                        village.Feeders = new List<Feeder> { feeder };
                    }
                    else
                    {
                        village.Feeders = new List<Feeder>();
                    }

                    var villages = taluk.Villages != null ? taluk.Villages.ToList() : new List<Village>();
                    villages.Add(village);
                    taluk.Villages = villages;
                }
            }

            System.IO.File.WriteAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"), JsonConvert.SerializeObject(states));
        }
    }
}
