using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TemplateParser.Tests
{
    [TestClass]
    public class BinaryIteratorTests
    {
        [TestMethod]
        public void CanIterateOverBinaryString()
        {
            var iterator = new BinaryIterator("00000001001000110100");

            Assert.AreEqual(0, iterator.GetBitSetValue(4));
            Assert.AreEqual(8, iterator.GetBitSetValue(4));
            Assert.AreEqual(4, iterator.GetBitSetValue(4));
            Assert.AreEqual(12, iterator.GetBitSetValue(4));
            Assert.AreEqual(2, iterator.GetBitSetValue(4));
        }

        [TestMethod]
        public void CannotIteratePastThenEndOfTheString()
        {
            var iterator = new BinaryIterator("1111");

            Assert.AreEqual(0, iterator.GetBitSetValue(6));
            Assert.AreEqual(0, iterator.GetBitSetValue(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CannotAskForLessThanZeroBits()
        {
            var iterator = new BinaryIterator("1111");

            iterator.GetBitSetValue(0);
        }
    }
}
