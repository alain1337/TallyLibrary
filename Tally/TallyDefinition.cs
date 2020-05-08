using System;

namespace Tally
{
    public class TallyDefinition
    {
        public string Caption { get; }

        public TallyDefinition(string caption, TallyBin[] bins)
        {
            Bins = bins;
            Caption = caption;
        }

        public TallyBin[] Bins { get; }
    }
}
