using System;
using System.Collections.Generic;
using System.Text;

namespace Tally
{
    public interface ITally<T>
    {
        public TallyDefinition<T> Definition { get; }
        TallyCount<T> Tally(IEnumerable<T> items);
    }
}
