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
    public class TransformerRepository : ITransformerRepository
    {
        private readonly Configuration configuration;

        public TransformerRepository(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public void AddTransformer(string type, int typeId, Transformer transformer)
        {
            var data = System.IO.File.ReadAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"));
            var states = JsonConvert.DeserializeObject<List<State>>(data);
            if (type.Equals("Village", StringComparison.InvariantCultureIgnoreCase))
            {
                var village = states.SelectMany(p => p.Districts.SelectMany(q => q.Taluks.SelectMany(r => r.Villages))).FirstOrDefault(p => p.Id == typeId);
                if (village != null)
                {
                    var feeder = village.Feeders.FirstOrDefault(p => p.Id == transformer.Id);
                    if (feeder != null)
                    {
                        var transformers = feeder.Transformers != null
                            ? feeder.Transformers.ToList()
                            : new List<Transformer>();
                        transformer.Id = FeederExtensions.GetNextTransformerId(states);
                        transformers.Add(transformer);
                        feeder.Transformers = transformers;
                    }
                }
            }
            else if (type.Equals("Taluk", StringComparison.InvariantCultureIgnoreCase))
            {
                var taluk = states.SelectMany(p => p.Districts.SelectMany(q => q.Taluks)).FirstOrDefault(p => p.Id == typeId);
                if (taluk != null)
                {
                    var feeder = taluk.Feeders.FirstOrDefault(p => p.Id == transformer.Id);
                    if (feeder != null)
                    {
                        var transformers = feeder.Transformers != null
                            ? feeder.Transformers.ToList()
                            : new List<Transformer>();
                        transformer.Id = FeederExtensions.GetNextTransformerId(states);
                        transformers.Add(transformer);
                        feeder.Transformers = transformers;
                    }
                }
            }
            else if (type.Equals("District", StringComparison.InvariantCultureIgnoreCase))
            {
                var district = states.SelectMany(p => p.Districts).FirstOrDefault(p => p.Id == typeId);
                if (district != null)
                {
                    var feeder = district.Feeders.FirstOrDefault(p => p.Id == transformer.Id);
                    if (feeder != null)
                    {
                        var transformers = feeder.Transformers != null
                            ? feeder.Transformers.ToList()
                            : new List<Transformer>();
                        transformer.Id = FeederExtensions.GetNextTransformerId(states);
                        transformers.Add(transformer);
                        feeder.Transformers = transformers;
                    }
                }
            }

            System.IO.File.WriteAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "Address.json"), JsonConvert.SerializeObject(states));
        }
    }
}
