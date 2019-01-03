using Data.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository.Interfaces
{
    public interface ITransformerRepository
    {
        void AddTransformer(string type, int typeId, Transformer transformer);
    }
}
