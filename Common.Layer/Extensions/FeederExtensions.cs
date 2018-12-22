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

        public static int GetNextTransformerId(IEnumerable<State> states)
        {
            var districTransformers = states.SelectMany(p => p.Districts.SelectMany(q => q.Feeders.SelectMany(r => r.Transformers))).Where(p => p != null);
            var talukaTransformers = states.SelectMany(p => p.Districts.SelectMany(q => q.Taluks.SelectMany(r => r.Feeders.SelectMany(s => s.Transformers)))).Where(p => p != null);
            var villageTransformers = states.SelectMany(p => p.Districts.SelectMany(q => q.Taluks.SelectMany(r => r.Villages.SelectMany(s => s.Feeders.SelectMany(a => a.Transformers))))).Where(p => p != null);
            var x = new List<int> { districTransformers.Any() ? districTransformers.Max(p => p.Id) : 0,
                talukaTransformers.Any() ? talukaTransformers.Max(p => p.Id) : 0,
                villageTransformers.Any() ? villageTransformers.Max(p => p.Id) : 0 }.Max();
            return x + 1;
        }
    }
}
