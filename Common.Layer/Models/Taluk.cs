using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Layer.Models
{
    public class Taluk
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Village> Villages { get; set; }
    }
}
