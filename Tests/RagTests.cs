using System;
using System.Linq;
using Tally.Tallies;
using Xunit;

namespace Tests
{
    public class RagTests
    {
        [Fact]
        public void Basics()
        {
            var tally = new RagTally<double>(d => d < 0.8 ? RagTallyBins.Red : d < 0.95 ? RagTallyBins.Amber : RagTallyBins.Green);
            const int count = 1_000_000;
            var rnd = new Random(1966);
            var counts = tally.Tally((Enumerable.Range(0, count).Select(i => rnd.NextDouble())));
            Assert.Equal(count, counts.Count);
            Assert.InRange(counts.Percentages[0] < 0.81).;
            Assert.IsTrue(counts.Percentages[1] < 0.16);
            Assert.IsTrue(counts.Percentages[2] < 0.06);
        }
    }
}
