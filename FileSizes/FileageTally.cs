using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tally.Tallies;

namespace FileSizes
{
    public class FileageTally : HistogramTally<FileInfo, int>
    {
        public FileageTally(string caption = null, bool upperClip = false)
            : base(Bins, AgeInDays, caption ?? "File Ages", upperClip: upperClip)
        {
        }

        static int AgeInDays(FileSystemInfo fi)
        {
            return (int)Math.Max(0, (DateTime.Today - fi.LastWriteTime).TotalDays);
        }

        static readonly HistogramBin<int>[] Bins =
        {
            new HistogramBin<int>(1, "Today"),
            new HistogramBin<int>(2, "Yesterday"),
            new HistogramBin<int>(7, "This week"),
            new HistogramBin<int>(14, "Last week"),
            new HistogramBin<int>(30, "This month"),
            new HistogramBin<int>(60, "Last month"),
            new HistogramBin<int>(365, "This year"),
            new HistogramBin<int>(730, "Last year"),
            new HistogramBin<int>(0, "Older")
        };
    }
}
