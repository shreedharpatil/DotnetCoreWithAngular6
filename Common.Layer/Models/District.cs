using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Layer.Models
{
    public class District
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Taluk> Taluks { get; set; }

        public IEnumerable<Feeder> Feeders { get; set; }
    }
}
