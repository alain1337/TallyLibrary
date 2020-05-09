using System;
using System.Collections.Generic;

namespace Tally
{
    public abstract class TallyBase<T> : ITally<T>
    {
        public TallyDefinition Definition { get; protected set; }
        public abstract int BinSelector(T item);
    }
}