using System;
using System.Collections.Generic;
using System.Text;

namespace Tally
{
    public interface ITally<in T>
    {
        public TallyDefinition Definition { get; }
        public int BinSelector(T item);

        TallyCount CreateTally(T item);
        TallyCount CreateTally(IEnumerable<T> items);
        TallyCount UpdateTally(TallyCount count, T item);
        TallyCount UpdateTally(TallyCount count, IEnumerable<T> items);
    }
}
