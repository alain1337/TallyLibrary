using System;
using System.Collections.Generic;
using System.Text;

namespace Tally
{
    public interface ITally<in T>
    {
        public TallyDefinition Definition { get; }
        public int BinSelector(T item);
    }
}
