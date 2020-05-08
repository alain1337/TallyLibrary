using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tally;

namespace FileSizes
{
    internal static class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("usage: FileSizes dir");
                return 9;
            }

            var sizes = new FilesizeTally();
            var sizeCounts = sizes.CreateCount();
            var extensions = new ExtensionTally();
            var extCounts = extensions.CreateCount();
            Do(new[] { sizeCounts, extCounts }, args[0]);
            Render(sizeCounts);
            Render(extCounts, 20);
            return 0;
        }

        static void Render<T>(TallyCount<T> tally, int? top = null)
        {
            Console.WriteLine(tally.Definition.Caption);
            Console.WriteLine();
            for (var i = 0; i < tally.Definition.Bins.Length; i++)
                Console.WriteLine($"\t{tally.Definition.Bins[i].Caption,-10} {tally.Counts[i],5} [{PercentBar(tally.Percentages[i])}]");
            Console.WriteLine($"\t{"TOTAL",-10} {tally.Count,5}");
            Console.WriteLine();
        }

        static string PercentBar(double p)
        {
            var c = (int)Math.Max(0, Math.Round(40 * p));
            return new string('#', c) + new string('_', 40 - c);
        }

        static void Do(TallyCount<FileInfo>[] tallies, string dir)
        {
            var fis = Directory.GetFiles(dir).Select(name => new FileInfo(name));
            tallies.Tally(fis);
            foreach (var subdir in Directory.GetDirectories(dir))
                Do(tallies, subdir);
        }
    }
}
