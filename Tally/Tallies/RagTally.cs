using System;

namespace Tally.Tallies
{
    public class RagTally<T> : TallyBase<T>, ITally<T>
    {
        public RagTally(Func<T, RagTallyBins> binSelector, string caption = null)
        {
            Definition = new TallyDefinition<T>(caption ?? "RAG", new[]
            {
                new TallyBin("Red"),
                new TallyBin("Amber"),
                new TallyBin("Green")
            }, item => (int)binSelector(item));
        }
    }

    public enum RagTallyBins
    {
        Red = 0,
        Amber = 1,
        Green = 2
    }
}
