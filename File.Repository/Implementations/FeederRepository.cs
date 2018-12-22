using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Layer.Extensions;
using Common.Layer.Models;
using File.Repository.Interfaces;
using Newtonsoft.Json;

namespace File.Repository.Implementations
{
    public class FeederRepository : IFeederRepository
    {
        private readonly Configuration configuration;

        public FeederRepository(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public void AddFeeder(string type, Feeder feeder)
        {
            feeder.Transformers = new List<Transformer>();
            var data = System.IO.File.ReadAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"));
            var states = JsonConvert.DeserializeObject<List<State>>(data);
            if (type.Equals("Village", StringComparison.InvariantCultureIgnoreCase))
            {
                var village = states.SelectMany(p => p.Districts.SelectMany(q => q.Taluks.SelectMany(r => r.Villages))).FirstOrDefault(p => p.Id == feeder.Id);
                if (village != null)
                {
                    var feeders = village.Feeders != null ? village.Feeders.ToList() : new List<Feeder>();
                    feeder.Id = FeederExtensions.GetNextFeederId(states);
                    feeders.Add(feeder);
                    village.Feeders = feeders;
                }
            }
            else if (type.Equals("Taluk", StringComparison.InvariantCultureIgnoreCase))
            {
                var taluk = states.SelectMany(p => p.Districts.SelectMany(q => q.Taluks)).FirstOrDefault(p => p.Id == feeder.Id);
                if (taluk != null)
                {
                    var feeders = taluk.Feeders != null ? taluk.Feeders.ToList() : new List<Feeder>();
                    feeder.Id = FeederExtensions.GetNextFeederId(states);
                    feeders.Add(feeder);
                    taluk.Feeders = feeders;
                }
            }
            else if (type.Equals("District", StringComparison.InvariantCultureIgnoreCase))
            {
                var district = states.SelectMany(p => p.Districts).FirstOrDefault(p => p.Id == feeder.Id);
                if (district != null)
                {
                    var feeders = district.Feeders != null ? district.Feeders.ToList() : new List<Feeder>();
                    feeder.Id = FeederExtensions.GetNextFeederId(states);
                    feeders.Add(feeder);
                    district.Feeders = feeders;
                }
            }

            System.IO.File.WriteAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"), JsonConvert.SerializeObject(states));
        }
    }
}
