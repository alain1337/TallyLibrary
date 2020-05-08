using System;
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

            var tally = new FilesizeTally();
            var counts = tally.CreateCount();
            Do(counts, args[0]);
            Render(counts);
            return 0;
        }

        static void Render<T>(TallyCount<T> tally)
        {
            Console.WriteLine(tally.Definition.Caption);
            for (int i = 0; i < tally.Definition.Bins.Length; i++)
                Console.WriteLine($"\t{tally.Definition.Bins[i].Caption,-10} {tally.Counts[i],5} [{PercentBar(tally.Percentages[i])}]");
            Console.WriteLine();
        }

        static string PercentBar(double p)
        {
            var c = (int)Math.Max(0, Math.Round(40 * p));
            return new string('#', c) + new string('_', 40 - c);
        }

        static void Do(TallyCount<FileInfo> tally, string dir)
        {
            tally.Tally(Directory.GetFiles(dir).Select(name => new FileInfo(name)));
            foreach (var subdir in Directory.GetDirectories(dir))
                Do(tally, subdir);
        }
    }
}
