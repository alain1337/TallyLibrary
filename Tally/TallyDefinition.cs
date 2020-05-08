using System;

namespace Tally
{
    public class TallyDefinition<T>
    {
        public string Caption { get; }

        public TallyDefinition(string caption, TallyBin[] bins, Func<T,int> binSelector)
        {
            Bins = bins;
            Caption = caption;
            BinSelector = binSelector;
        }

        public int AddBin(TallyBin bin)
        {
            Array.Resize(ref Bins, (Bins?.Length ?? 0) + 1);
            Bins[^1] = bin;
            return Bins.Length - 1;
        }

        public TallyBin[] Bins;
        public readonly Func<T, int> BinSelector;
    }
}
