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
        public TallyBin[] Bins { get; }

        public Func<T, int> BinSelector { get; }
    }
}
