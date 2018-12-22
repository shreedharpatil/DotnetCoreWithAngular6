using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Layer.Models;

namespace Common.Layer.Extensions
{
    public class FeederExtensions
    {
        public static int GetNextFeederId(IEnumerable<State> states)
        {
            var stateFeeders = states.SelectMany(p => p.Districts.SelectMany(q => q.Feeders));
            var talukFeeders = states.SelectMany(p => p.Districts.SelectMany(q => q.Taluks.SelectMany(r => r.Feeders)));
            var villageFeeders = states.SelectMany(p => p.Districts.SelectMany(q => q.Taluks.SelectMany(r => r.Villages.SelectMany(s => s.Feeders))));
            var x = new List<int> { stateFeeders.Any() ? stateFeeders.Max(p => p.Id) : 0,
                talukFeeders.Any() ? talukFeeders.Max(p => p.Id) : 0,
                villageFeeders.Any() ? villageFeeders.Max(p => p.Id) : 0 }.Max();
            return x + 1;
        }
    }
}
