using Data.Repository.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface IAddressRepository
    {
        void AddState(State state);

        void AddDistrict(string stateId, District district);

        IEnumerable<State> GetStates();

        IEnumerable<District> GetDistricts(string stateId);

        Task AddTaluk(string stateId, string districtId, Taluk taluk);

        Task AddVillage(string stateId, string districtId, string talukId, Village village);
    }
}
