using System;
using System.IO;
using Tally;
using Tally.Tallies;

namespace FileSizes
{
    public class ExtensionTally : TallyBase<FileInfo>, ITally<FileInfo>
    {
        public ExtensionTally()
        {
            Definition = new TallyDefinition("Extensions", new[] { new TallyBin("(none)") });
        }

        public override int BinSelector(FileInfo fi)
        {
            var ext = fi.Extension;
            if (String.IsNullOrEmpty(ext) || ext.Length > 10) 
                return 0;
            ext = ext.ToLower();
            var i = Array.FindIndex(Definition.Bins, b => b.Caption == ext);
            if (i < 0)
                i = Definition.AddBin(new TallyBin(ext));
            return i;
        }
    }
}
