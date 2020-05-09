using System;

namespace Tally.Tallies
{
    public class RagTally<T> : TallyBase<T>
    {
        public RagTally(Func<T, RagTallyBins> binSelector, string caption = null)
        {
            Definition = new TallyDefinition(caption ?? "RAG", new[]
            {
                new TallyBin("Red"),
                new TallyBin("Amber"),
                new TallyBin("Green")
            });
            _binSelector = binSelector;
        }

        readonly Func<T, RagTallyBins> _binSelector;
        public override int BinSelector(T item)
        {
            return (int)_binSelector(item);
        }
    }

    public enum RagTallyBins
    {
        Red = 0,
        Amber = 1,
        Green = 2
    }
}
