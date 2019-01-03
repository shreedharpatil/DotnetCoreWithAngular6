using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository.Models
{
    public class District
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public IList<Taluk> Taluks { get; set; }

        public IList<Feeder> Feeders { get; set; }
    }
}
