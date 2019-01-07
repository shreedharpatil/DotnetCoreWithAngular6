using System;
using System.Collections.Generic;
using System.Text;
using Common.Layer.Models;

namespace File.Repository.Interfaces
{
    public interface IFeederRepository
    {
        void AddFeeder(string type, int typeId, Feeder feeder);
    }
}
