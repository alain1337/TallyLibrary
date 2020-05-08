using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            const int count = 1_000;
            var counts = tally.Tally(Enumerable.Range(0, 1000));
            Assert.AreEqual(count, counts.Count);
            Assert.AreEqual(count / 2, counts.Counts[0]);
            Assert.AreEqual(count / 2, counts.Counts[1]);
        }
    }
}