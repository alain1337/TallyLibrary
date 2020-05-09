using System;

namespace Tally
{
    public class TallyDefinition
    {
        public string Caption { get; }
        public TallyBin[] Bins;

        public TallyDefinition(string caption, TallyBin[] bins)
        {
            Caption = caption;
            Bins = bins;
        }

        public int AddBin(TallyBin bin)
        {
            Array.Resize(ref Bins, (Bins?.Length ?? 0) + 1);
            Bins[^1] = bin;
            return Bins.Length - 1;
        }

        public int FindBinIndex(string binCaption)
        {
            return Array.FindIndex(Bins, b => String.Equals(b.Caption, binCaption));
        }
    }
}
