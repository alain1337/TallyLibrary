using System;
using System.Collections.Generic;
using System.Text;

namespace Tally.Tallies
{
    public class RagTally<T> : TallyBase<T>, ITally<T>
    {
        public RagTally(Func<T, RagTallyBins> binSelector, string caption = null)
        {
            Definition = new TallyDefinition(caption ?? "RAG", new[]
            {
                new TallyBin("Red", true, false),
                new TallyBin("Amber", true, false),
                new TallyBin("Green", true, false)
            });

            BinSelector = item => (int)binSelector(item);
        }
    }

    public enum RagTallyBins
    {
        Red = 0,
        Amber = 1,
        Green = 2
    }
}
