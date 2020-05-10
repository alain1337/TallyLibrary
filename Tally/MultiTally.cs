using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tally
{
    public class MultiTally<T>
    {
        public IReadOnlyList<ITally<T>> Tallies { get; private set; }
        public IReadOnlyList<TallyCount> Counts { get; private set; }

        public MultiTally(params ITally<T>[] tallies)
        {
            Tallies = tallies.ToList().AsReadOnly();
            Counts = tallies.Select(t => t.CreateCount()).ToList().AsReadOnly();
        }

        public int Count => Tallies.Count;
    }


    public static class MultiTallyExtensions
    {
        public static void UpdateTally<T>(this MultiTally<T> tallies, T item)
        {
            for (int i = 0; i < tallies.Tallies.Count; i++)
                tallies.Tallies[i].UpdateTally(tallies.Counts[i], item);
        }

        public static void UpdateTally<T>(this MultiTally<T> tallies, IEnumerable<T> items)
        {
            foreach (var item in items)
                UpdateTally(tallies, item);
        }
    }
}
