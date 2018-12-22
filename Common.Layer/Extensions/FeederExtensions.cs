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
            var enumerable = states as State[] ?? states.ToArray();
            var sts = enumerable.ToList();

            var dts = sts.Where(p => p.Districts != null).SelectMany(p => p.Districts).Where(p => p.Feeders != null).SelectMany(p => p.Feeders)
                .ToList();

            var tks = sts.Where(p => p.Districts != null).SelectMany(p => p.Districts).Where(p => p.Taluks != null).SelectMany(p => p.Taluks)
                .Where(p => p.Feeders != null).SelectMany(p => p.Feeders).ToList();

            var vls = sts.Where(p => p.Districts != null).SelectMany(p => p.Districts).Where(p => p.Taluks != null).SelectMany(p => p.Taluks)
                .Where(p => p.Villages != null).SelectMany(p => p.Villages).Where(p => p.Feeders != null).SelectMany(p => p.Feeders)
                .ToList();

            var x = new List<int> { dts.Any() ? dts.Max(p => p.Id) : 0,
                tks.Any() ? tks.Max(p => p.Id) : 0,
                vls.Any() ? vls.Max(p => p.Id) : 0 }.Max();
            return x + 1;
        }

        public static int GetNextTransformerId(IEnumerable<State> states)
        {
            var enumerable = states as State[] ?? states.ToArray();
            var sts = enumerable.ToList();

            var dts = sts.Where(p => p.Districts != null).SelectMany(p => p.Districts).Where(p => p.Feeders != null).SelectMany(p => p.Feeders)
                .Where(p => p.Transformers != null).SelectMany(p => p.Transformers).ToList();

            var tks = sts.Where(p => p.Districts != null).SelectMany(p => p.Districts).Where(p => p.Taluks != null).SelectMany(p => p.Taluks)
                .Where(p => p.Feeders != null).SelectMany(p => p.Feeders)
                .Where(p => p.Transformers != null).SelectMany(p => p.Transformers).ToList();

            var vls = sts.Where(p => p.Districts != null).SelectMany(p => p.Districts).Where(p => p.Taluks != null).SelectMany(p => p.Taluks)
                .Where(p => p.Villages != null).SelectMany(p => p.Villages).Where(p => p.Feeders != null).SelectMany(p => p.Feeders)
                .Where(p => p.Transformers != null).SelectMany(p => p.Transformers).ToList();
            
            var x = new List<int> { dts.Any() ? dts.Max(p => p.Id) : 0,
                tks.Any() ? tks.Max(p => p.Id) : 0,
                vls.Any() ? vls.Max(p => p.Id) : 0 }.Max();
            return x + 1;
        }
    }
}
