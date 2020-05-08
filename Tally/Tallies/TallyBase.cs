using System;
using System.Collections.Generic;

namespace Tally.Tallies
{
    public abstract class TallyBase<T>
    {
        protected Func<T, int> BinSelector;
        public TallyDefinition Definition { get; protected set; }

        public TallyCount Tally(IEnumerable<T> items)
        {
            var c = new TallyCount(Definition);
            c.Tally<T>(items, BinSelector);
            return c;
        }
    }
}