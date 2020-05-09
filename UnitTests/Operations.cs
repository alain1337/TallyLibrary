using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tally;

namespace UnitTests
{
    [TestClass]
    public class Operations
    {
        [TestMethod]
        public void Add()
        {
            var c1 = new TallyCount(null, new[] {1, 2, 3});
            var c2 = new TallyCount(null, new[] {1, 2, 3});
            var sum = c1 + c2;
            Assert.AreEqual(2, sum.Counts[0]);
            Assert.AreEqual(4, sum.Counts[1]);
            Assert.AreEqual(6, sum.Counts[2]);
        }
    }
}
