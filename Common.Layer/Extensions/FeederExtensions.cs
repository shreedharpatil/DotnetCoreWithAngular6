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
            var tcs = GetFeeders(states.ToList()).ToList();
            return tcs.Any() ? (tcs.Max(p => p.Id) + 1) : 1;
        }

        public static int GetNextTransformerId(IEnumerable<State> states)
        {
            var tcs = GetTransformers(states.ToList()).ToList();
            return tcs.Any() ? (tcs.Max(p => p.Id) + 1) : 1;
        }

        public static IEnumerable<Transformer> GetTransformers(IList<State> states)
        {
            var dts = states.Where(p => p.Districts != null).SelectMany(p => p.Districts).Where(p => p.Feeders != null).SelectMany(p => p.Feeders)
                .Where(p => p.Transformers != null).SelectMany(p => p.Transformers).ToList();

            var tks = states.Where(p => p.Districts != null).SelectMany(p => p.Districts).Where(p => p.Taluks != null).SelectMany(p => p.Taluks)
                .Where(p => p.Feeders != null).SelectMany(p => p.Feeders)
                .Where(p => p.Transformers != null).SelectMany(p => p.Transformers).ToList();

            var vls = states.Where(p => p.Districts != null).SelectMany(p => p.Districts).Where(p => p.Taluks != null).SelectMany(p => p.Taluks)
                .Where(p => p.Villages != null).SelectMany(p => p.Villages).Where(p => p.Feeders != null).SelectMany(p => p.Feeders)
                .Where(p => p.Transformers != null).SelectMany(p => p.Transformers).ToList();

            dts.AddRange(tks);
            dts.AddRange(vls);

            return dts;
        }

        public static IEnumerable<Feeder> GetFeeders(IList<State> states)
        {
            var dts = states.Where(p => p.Districts != null).SelectMany(p => p.Districts).Where(p => p.Feeders != null).SelectMany(p => p.Feeders)
                .ToList();

            var tks = states.Where(p => p.Districts != null).SelectMany(p => p.Districts).Where(p => p.Taluks != null).SelectMany(p => p.Taluks)
                .Where(p => p.Feeders != null).SelectMany(p => p.Feeders).ToList();

            var vls = states.Where(p => p.Districts != null).SelectMany(p => p.Districts).Where(p => p.Taluks != null).SelectMany(p => p.Taluks)
                .Where(p => p.Villages != null).SelectMany(p => p.Villages).Where(p => p.Feeders != null).SelectMany(p => p.Feeders)
                .ToList();

            dts.AddRange(tks);
            dts.AddRange(vls);

            return dts;
        }
    }
}
