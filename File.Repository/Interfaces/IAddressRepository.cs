using Common.Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace File.Repository.Interfaces
{
    public interface IAddressRepository
    {
        void AddState(State state);

        void AddDistrict(int stateId, District district);

        IEnumerable<State> GetStates();

        IEnumerable<District> GetDistricts(int stateId);
    }
}
