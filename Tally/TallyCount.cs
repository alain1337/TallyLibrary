using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace Tally
{
    public class TallyCount
    {
        public TallyDefinition Definition { get; private set; }
        public int[] Counts { get; private set; }
        public int Count => Counts.Sum();
        public double[] Percentages => Counts.Select(c => Count > 0 ? (double)c / Count : 0.0).ToArray();

        public void Tally<T>(IEnumerable<T> items, Func<T, int> binSelector)
        {
            foreach (var item in items)
                Counts[binSelector(item)]++;
        }

        public static TallyCount operator +(TallyCount c1, TallyCount c2)
        {
            if (c1.Definition != c2.Definition)
                throw new InvalidOperationException($"Cannot add different tallies: {c1.Definition.Caption} vs. {c2.Definition.Caption}");

            return new TallyCount(c1.Definition, Enumerable.Range(0, c1.Counts.Length).Select(i => c1.Counts[i] + c2.Counts[i]));
        }

        public void Add(TallyCount c2)
        {
            Enumerable.Range(0, Counts.Length).Select(i => Counts[i] += c2.Counts[i]);
        }

        public TallyCount(TallyDefinition definition)
        {
            Definition = definition;
            Counts = new int[Definition.Bins.Length];
        }

        public TallyCount(TallyDefinition definition, IEnumerable<int> counts)
        {
            Definition = definition;
            Counts = counts.ToArray();
        }
    }
}
