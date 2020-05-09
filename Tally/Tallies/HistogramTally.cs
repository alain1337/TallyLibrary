using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tally.Tallies
{
    public class HistogramTally<TItem, TValue> : TallyBase<TItem> where TValue : IComparable<TValue>
    {
        public HistogramTally(IEnumerable<HistogramBin<TValue>> bins, Func<TItem, TValue> valueExtractor, string caption = null, TValue lowerBound = default(TValue), bool upperClip = false)
        {
            HistogramBins = bins.ToArray();
            _valueExtractor = valueExtractor;
            Definition = new TallyDefinition(caption ?? "Histogram", HistogramBins);
            LowerBound = lowerBound;
            UpperClip = upperClip;
        }

        readonly Func<TItem, TValue> _valueExtractor;
        public HistogramBin<TValue>[] HistogramBins { get; }
        public TValue LowerBound { get; }
        public bool UpperClip { get; }

        public override int BinSelector(TItem item)
        {
            var value = _valueExtractor(item);
            if (value.CompareTo(LowerBound) < 0)
                return -1;
            for (var i=0; i < HistogramBins.Length; i++)
                if (value.CompareTo(HistogramBins[i].UpperBound) < 0)
                    return i;
            return UpperClip ? -1 : HistogramBins.Length - 1;
        }
    }

    public class HistogramBin<T> : TallyBin
    {
        public T UpperBound { get; }

        public HistogramBin(T upperBound, string caption = null) : base(caption ?? "< " + upperBound)
        {
            UpperBound = upperBound;
        }
    }
}
