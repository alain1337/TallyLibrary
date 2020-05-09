using System;
using System.Collections.Generic;
using System.Text;

namespace Tally
{
    public static class TallyExtensions
    {
        public static TallyCount CreateTally<T>(this ITally<T> tally, T item)
        {
            return UpdateTally(tally, new TallyCount(tally.Definition), item);
        }

        public static TallyCount CreateTally<T>(this ITally<T> tally, IEnumerable<T> items)
        {
            var count = new TallyCount(tally.Definition);
            foreach (var item in items)
                UpdateTally(tally, count, item);
            return count;
        }

        public static TallyCount UpdateTally<T>(this ITally<T> tally, TallyCount count, T item)
        {
            var i = tally.BinSelector(item);
            if (i < 0)
                return count;
            if (i >= tally.Definition.Bins.Length)
                throw new Exception($"BinSelector() returned invalid value: {i} ({tally.Definition.Bins.Length} bins exist");
            if (count.Counts.Length != tally.Definition.Bins.Length)
                Array.Resize(ref count.Counts, tally.Definition.Bins.Length);
            count.Counts[i]++;
            return count;
        }

        public static TallyCount UpdateTally<T>(this ITally<T> tally, TallyCount count, IEnumerable<T> items)
        {
            foreach (var item in items)
                UpdateTally(tally, count, item);
            return count;
        }

        public static TallyCount CreateCount<T>(this ITally<T> tally) => new TallyCount(tally.Definition);
    }
}
