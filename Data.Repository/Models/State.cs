using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository.Models
{
    public class State
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public IList<District> Districts { get; set; }
    }
}
