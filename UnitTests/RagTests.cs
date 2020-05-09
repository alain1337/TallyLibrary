using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tally.Tallies;

namespace UnitTests
{
    [TestClass]
    public class RagTests
    {
        [TestMethod]
        public void Basics()
        {
            var tally = new RagTally<double>(d => d < 0.8 ? RagTallyBins.Red : d < 0.95 ? RagTallyBins.Amber : RagTallyBins.Green);
            const int samples = 1_000_000;
            var rnd = new Random(1966);
            var count = tally.CreateTally(Enumerable.Range(0, samples).Select(i => rnd.NextDouble()));
            Assert.AreEqual(count, count.Count);
            Assert.IsTrue(count.Percentages[0] < 0.81);
            Assert.IsTrue(count.Percentages[1] < 0.16);
            Assert.IsTrue(count.Percentages[2] < 0.06);
        }
    }
}
