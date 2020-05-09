using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Tally;
using Tally.Tallies;

namespace FileSizes
{
    public class FilesizeTally : HistogramTally<FileInfo, long>
    {
        public FilesizeTally(string caption = null, bool upperClip = false) 
            : base(Bins, fi => fi.Length, caption ?? "Filesizes", upperClip: upperClip)
        {
        }

        static readonly HistogramBin<long>[] Bins =
        {
            new HistogramBin<long>(10_000, "Tiny"),
            new HistogramBin<long>(100_000, "Small"),
            new HistogramBin<long>(1_000_000, "Medium"),
            new HistogramBin<long>(16_000_000, "Large"),
            new HistogramBin<long>(128_000_000, "Huge"),
            new HistogramBin<long>(0, "Gigantic")
        };
    }
}
