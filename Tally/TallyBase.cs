using System;
using System.Collections.Generic;

namespace Tally
{
    public abstract class TallyBase<T> : ITally<T>
    {
        public TallyDefinition Definition { get; protected set; }
        public abstract int BinSelector(T item);

        public TallyCount CreateTally(T item)
        {
            return UpdateTally(new TallyCount(Definition), item);
        }

        public TallyCount CreateTally(IEnumerable<T> items)
        {
            var count = new TallyCount(Definition);
            foreach (var item in items)
                UpdateTally(count, item);
            return count;
        }

        public TallyCount UpdateTally(TallyCount count, T item)
        {
            var i = BinSelector(item);
            if (i < 0)
                return count;
            if (i >= Definition.Bins.Length)
                throw new Exception($"BinSelector() returned invalid value: {i} ({Definition.Bins.Length} bins exist");
            if (count.Counts.Length != Definition.Bins.Length)
                Array.Resize(ref count.Counts, Definition.Bins.Length);
            count.Counts[i]++;
            return count;
        }

        public TallyCount UpdateTally(TallyCount count, IEnumerable<T> items)
        {
            foreach (var item in items)
                UpdateTally(count, item);
            return count;
        }

        public TallyCount CreateCount() => new TallyCount(Definition);
    }
}