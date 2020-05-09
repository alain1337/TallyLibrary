using System;
using System.Collections.Generic;
using System.Text;

namespace Tally.Tallies
{
    public class ValuesTally<TItem, TValue> : ITally<TItem> where TValue : IEquatable<TValue>
    {
        public ValuesTally(Func<TItem, TValue> valueExtractor, string caption = null)
        {
            Definition = new TallyDefinition("Values", new[] { new TallyBin("(none)") });
            _valueExtractor = valueExtractor;
        }

        public TallyDefinition Definition { get; }
        readonly Func<TItem, TValue> _valueExtractor;

        public virtual int BinSelector(TItem fi)
        {
            var value = _valueExtractor(fi);
            if (value == null)
                return 0;
            var caption = value.ToString();
            var i = Array.FindIndex(Definition.Bins, b => b.Caption == caption);
            if (i < 0)
                i = Definition.AddBin(new TallyBin(caption));
            return i;
        }
    }
}
