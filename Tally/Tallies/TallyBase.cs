using System;
using System.Collections.Generic;

namespace Tally.Tallies
{
    public abstract class TallyBase<T>
    {
        public TallyDefinition<T> Definition { get; protected set; }

        public TallyCount<T> Tally(IEnumerable<T> items)
        {
            var c = new TallyCount<T>(Definition);
            c.Tally(items);
            return c;
        }

        public TallyCount<T> CreateCount() => new TallyCount<T>(Definition);
    }
}