using System;
using System.Collections.Generic;
using System.Linq;

namespace Tally
{
    public class TallyCount
    {
        public TallyDefinition Definition { get; }
        public string Caption => Definition.Caption;
        public int[] Counts;
        public int Count => Counts.Sum();
        public double[] Percentages => Counts.Select(c => Count > 0 ? (double)c / Count : 0.0).ToArray();

        public static TallyCount operator +(TallyCount c1, TallyCount c2)
        {
            if (c1.Definition != c2.Definition)
                throw new InvalidOperationException($"Cannot add different tallies: {c1.Definition.Caption} vs. {c2.Definition.Caption}");

            return new TallyCount(c1.Definition, Enumerable.Range(0, c1.Counts.Length).Select(i => c1.Counts[i] + c2.Counts[i]));
        }

        public void Add(TallyCount c2)
        {
            Enumerable.Range(0, Counts.Length).Select(i => Counts[i] += c2.Counts[i]).ToList();
        }

        public TallyBinInfo this[string binCaption] 
        {
            get
            {
                var i = Definition.FindBinIndex(binCaption);
                if (i < 0)
                    throw new IndexOutOfRangeException(nameof(binCaption));
                return CreateBinInfo(i);
            }
        }

        TallyBinInfo CreateBinInfo(int i)
        {
            return new TallyBinInfo(i, Definition.Bins[i].Caption, Counts[i], Count);
        }

        public IEnumerable<TallyBinInfo> GetBinInfos()
        {
            for (var i = 0; i < Definition.Bins.Length; i++)
                yield return CreateBinInfo(i);
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

    public class TallyBinInfo
    {
        public int Index { get; }
        public string Caption { get; }
        public int Count { get; }
        public int TotalCount { get; }

        public double Percentage => TotalCount > 0 ? (double) Count / TotalCount : 0.0;

        internal TallyBinInfo(int index, string caption, int count, int totalCount)
        {
            Index = index;
            Caption = caption;
            Count = count;
            TotalCount = totalCount;
        }
    }
}
