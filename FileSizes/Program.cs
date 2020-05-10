using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var ages = new FileageTally();
            var ageCounts = ages.CreateCount();
            var allTallies = new ITally<FileInfo>[] { sizes, ages, extensions, backupTodo };
            var allCounts = new[] { sizeCounts, ageCounts, extCounts, backupCounts };

            var traverser = new DirectoryTraverser();
            traverser.OnFile += fi =>
            {
                for (var i = 0; i < allTallies.Length; i++)
                    allTallies[i].UpdateTally(allCounts[i], fi);
            };
            var tr = traverser.Traverse(args[0]);
            Console.WriteLine($"Scanned {tr.Directories} directories and {tr.Files} files in {tr.Elapsed} ({tr.Exceptions} exceptions)");
            Console.WriteLine();

            Render(sizeCounts);
            Render(ageCounts);
            Render(extCounts, 10);
            RenderCompletion(backupCounts);

            return 0;
        }

        static void Render(TallyCount tally, int? top = null)
        {
            var bins = tally.GetBinInfos().ToList();
            int others = 0;
            if (top < bins.Count)
            {
                bins = bins.OrderByDescending(b => b.Count).ToList();
                others = bins.Skip(top.Value).Sum(b => b.Count);
                bins = bins.Take(top.Value).ToList();
            }

            Console.WriteLine(tally.Definition.Caption + (others > 0 ? $" (Top {top})" : ""));
            Console.WriteLine();
            foreach (var bin in bins)
                Console.WriteLine($"\t{bin.Caption,-15} {bin.Count,5} [{PercentBar(bin.Percentage)}]");
            if (others > 0)
                Console.WriteLine($"\t{$"({tally.Counts.Length - top} other)",-15} {others,5} [{PercentBar((double)others / tally.Count)}]");
            Console.WriteLine($"\t{"TOTAL",-15} {tally.Count,5}");
            Console.WriteLine();
        }

        static string PercentBar(double p)
        {
            var c = (int)Math.Max(0, Math.Round(40 * p));
            return new string('#', c) + new string('_', 40 - c);
        }

        static void RenderCompletion(TallyCount count)
        {
            Console.WriteLine(count.Definition.Caption);
            Console.WriteLine();
            foreach (var line in BigLetters.Render($"{count.Percentages[^1]:P2}"))
                Console.WriteLine("\t" + line);
            Console.WriteLine();
        }
    }
}
