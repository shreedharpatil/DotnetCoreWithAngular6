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
    }
}
