using Data.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface IFeederRepository
    {
        Task AddFeeder(string type, string typeId, Feeder feeder);
    }
}
