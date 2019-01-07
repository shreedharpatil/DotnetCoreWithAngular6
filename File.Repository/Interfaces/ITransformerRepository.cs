using System;
using System.Collections.Generic;
using System.Text;
using Common.Layer.Models;

namespace File.Repository.Interfaces
{
    public interface ITransformerRepository
    {
        void AddTransformer(string type, int typeId, int feederId, Transformer transformer);
    }
}
