using Common.Layer.Models;
using File.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                var state = states.FirstOrDefault(p => p.Id == stateId);
                if(state.Districts!= null && state.Districts.Any())
                {
                    district.Id = state.Districts.Max(p => p.Id) + 1;                    
                }
                else
                {
                    district.Id = 1;
                }

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
                    if (district.Taluks != null && district.Taluks.Any())
                    {
                        taluk.Id = district.Taluks.Max(p => p.Id) + 1;
                    }
                    else
                    {
                        taluk.Id = 1;
                    }

                    var taluks = district.Taluks.ToList();
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
                    if (taluk.Villages != null && taluk.Villages.Any())
                    {
                        village.Id = taluk.Villages.Max(p => p.Id) + 1;
                    }
                    else
                    {
                        village.Id = 1;
                    }

                    var villages = taluk.Villages.ToList();
                    villages.Add(village);
                    taluk.Villages = villages;
                }
            }

            System.IO.File.WriteAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"), JsonConvert.SerializeObject(states));
        }
    }
}
