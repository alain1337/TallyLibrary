using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tally;
using Tally.Tallies;

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
            var backupTodo = new TodoDoneTally<FileInfo>(fi => !fi.Attributes.HasFlag(FileAttributes.Archive), "Backup Status");
            var backupCounts = backupTodo.CreateCount();

            Do(new [] { sizeCounts, extCounts, backupCounts }, args[0]);
            Render(sizeCounts);
            Render(extCounts, 10);
            RenderCompletion(backupCounts);
            return 0;
        }

        static void Render<T>(TallyCount<T> tally, int? top = null)
        {
            var indices = Enumerable.Range(0, tally.Counts.Length).ToList();
            var others = 0;
            if (top < indices.Count)
            {
                indices = indices.OrderByDescending(i => tally.Counts[i]).ToList();
                others = indices.Skip(top.Value).Sum(i => tally.Counts[i]);
                indices = indices.Take(top.Value).ToList();
            }

            Console.WriteLine(tally.Definition.Caption + (others > 0 ? $" (Top {top})" : ""));
            Console.WriteLine();
            foreach (var i in indices)
                Console.WriteLine($"\t{tally.Definition.Bins[i].Caption,-15} {tally.Counts[i],5} [{PercentBar(tally.Percentages[i])}]");
            if (others > 0)
                Console.WriteLine($"\t{$"({tally.Count - top} other)",-15} {others,5} [{PercentBar((double)others / tally.Count)}]");
            Console.WriteLine($"\t{"TOTAL",-15} {tally.Count,5}");
            Console.WriteLine();
        }

        static string PercentBar(double p)
        {
            var c = (int)Math.Max(0, Math.Round(40 * p));
            return new string('#', c) + new string('_', 40 - c);
        }

        static void RenderCompletion<T>(TallyCount<T> count)
        {
            Console.WriteLine(count.Definition.Caption);
            Console.WriteLine();
            foreach (var line in BigLetters.Render($"{count.Percentages[^1]:P2}"))
                Console.WriteLine(line);
            Console.WriteLine();
        }

        static void Do(TallyCount<FileInfo>[] tallies, string dir)
        {
            try
            {
                var fis = Directory.GetFiles(dir).Select(name => new FileInfo(name));
                tallies.Tally(fis);
                foreach (var subdir in Directory.GetDirectories(dir))
                    Do(tallies, subdir);
            }
            catch
            {
                // Just ignore
            }
        }
    }
}
