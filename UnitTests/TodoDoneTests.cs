using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tally;
using Tally.Tallies;

namespace UnitTests
{
    [TestClass]
    public class TodoDoneTests
    {
        [TestMethod]
        public void Basics()
        {
            var tally = new TodoDoneTally<int>(i => i % 2 == 0);
            const int samples = 1_000;
            var count = tally.CreateTally(Enumerable.Range(0, 1000));
            Assert.AreEqual(samples, count.Count);
            Assert.AreEqual(samples / 2, count.Counts[0]);
            Assert.AreEqual(samples / 2, count.Counts[1]);
        }
    }
}