﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

            var tallies = new MultiTally<FileInfo>(
                new FilesizeTally(),
                new FileageTally(),
                new ExtensionTally(),
                new TodoDoneTally<FileInfo>(fi => !fi.Attributes.HasFlag(FileAttributes.Archive), "Backup Status")
            );

            var traverser = new DirectoryTraverser();
            traverser.OnFile += fi => tallies.UpdateTally(fi);
            var tr = traverser.Traverse(args[0]);
            Console.WriteLine($"Scanned {tr.Directories} directories and {tr.Files} files in {tr.Elapsed} ({tr.Exceptions} exceptions)");
            Console.WriteLine();

            for (var i = 0; i < tallies.Count; i++)
            {
                switch (tallies.Tallies[i])
                {
                    case ExtensionTally _:
                        Render(tallies.Counts[i], 10);
                        break;
                    case TodoDoneTally<FileInfo> _:
                        RenderCompletion(tallies.Counts[i]);
                        break;
                    default:
                        Render(tallies.Counts[i]);
                        break;
                }
            }

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
                Console.WriteLine($"\u001b[32m\t{bin.Caption,-15} {bin.Count,10:N0} [{PercentBar(bin.Percentage)}]\u001b[0m");
            if (others > 0)
                Console.WriteLine($"\u001b[34m\t{$"({tally.Counts.Length - top} other)",-15} {others,10:N0} [{PercentBar((double)others / tally.Count)}]\u001b[0m");
            Console.WriteLine($"\u001b[97m\t{"TOTAL",-15} {tally.Count,10:N0}\u001b[0m");
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
                Console.WriteLine("\u001b[33m\t" + line + "\u001b[0m");
            Console.WriteLine();
        }
    }
}
