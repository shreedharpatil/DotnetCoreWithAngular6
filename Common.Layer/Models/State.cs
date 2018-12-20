using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Layer.Models
{
    public class State
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<District> Districts { get; set; }
    }
}
