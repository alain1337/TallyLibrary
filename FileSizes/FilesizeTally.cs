using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Tally;
using Tally.Tallies;

namespace FileSizes
{
    public class FilesizeTally : TallyBase<FileInfo>, ITally<FileInfo>
    {
        public FilesizeTally()
        {
            Definition = new TallyDefinition<FileInfo>("Filesizes", Bins, item => SizeBin.GetBin(item.Length, Bins));
        }

        static readonly SizeBin[] Bins =
        {
            new SizeBin("Tiny", 100),
            new SizeBin("Small", 1_000),
            new SizeBin("Big", 1_000_000),
            new SizeBin("Huge", 0)
        };
    }

    internal class SizeBin : TallyBin
    {
        readonly long _maxSize;
        internal SizeBin(string caption, long maxSize) : base(caption)
        {
            _maxSize = maxSize;
        }

        bool IsLess(long size) => size < _maxSize;

        internal static int GetBin(long size, SizeBin[] bins)
        {
            for (var i = 0; i < bins.Length - 1; i++)
                if (bins[i].IsLess(size))
                    return i;
            return bins.Length - 1;
        }
    }
}
