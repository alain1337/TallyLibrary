using System;
using System.Collections.Generic;
using System.Linq;

namespace Tally
{
    public class TallyCount<T>
    {
        public TallyDefinition<T> Definition { get; }
        public string Caption => Definition.Caption;
        public int[] Counts;
        public int Count => Counts.Sum();
        public double[] Percentages => Counts.Select(c => Count > 0 ? (double)c / Count : 0.0).ToArray();

        public void Tally(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                var i = Definition.BinSelector(item);
                if (Counts.Length != Definition.Bins.Length)
                    Array.Resize(ref Counts, Definition.Bins.Length);
                Counts[i]++;
            }
        }

        public static TallyCount<T> operator +(TallyCount<T> c1, TallyCount<T> c2)
        {
            if (c1.Definition != c2.Definition)
                throw new InvalidOperationException($"Cannot add different tallies: {c1.Definition.Caption} vs. {c2.Definition.Caption}");

            return new TallyCount<T>(c1.Definition, Enumerable.Range(0, c1.Counts.Length).Select(i => c1.Counts[i] + c2.Counts[i]));
        }

        public void Add(TallyCount<T> c2)
        {
            Enumerable.Range(0, Counts.Length).Select(i => Counts[i] += c2.Counts[i]).ToList();
        }

        public TallyCount(TallyDefinition<T> definition)
        {
            Definition = definition;
            Counts = new int[Definition.Bins.Length];
        }

        public TallyCount(TallyDefinition<T> definition, IEnumerable<int> counts)
        {
            Definition = definition;
            Counts = counts.ToArray();
        }
    }

    public static class TallyCountExtensions
    {
        public static void Tally<T>(this IEnumerable<TallyCount<T>> tallies, IEnumerable<T> items)
        {
            items = items.ToArray();
            foreach (var tally in tallies)
                tally.Tally(items);
        }
    }
}
