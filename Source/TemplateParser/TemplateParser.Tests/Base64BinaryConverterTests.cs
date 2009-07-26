using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TemplateParser.Tests
{
    [TestClass]
    public class Base64BinaryConverterTests
    {
        [TestMethod]
        public void CanValidateBase64String()
        {
            Assert.IsFalse(Base64BinaryConverter.ValidateBase64String("pad!£$%^&*()_+-={}[]:@~;'#<>?,./"));
            Assert.IsTrue(Base64BinaryConverter.ValidateBase64String("erjnwefasDWERYDFGQWefasdfwef"));
        }

        [TestMethod]
        public void CanCreateBinaryFromSimpleBase64String()
        {
            Assert.AreEqual("000000", Base64BinaryConverter.GetBinaryString("A"));
            Assert.AreEqual("100000", Base64BinaryConverter.GetBinaryString("B"));
        }

        [TestMethod]
        public void CanCreateBase64StringFromSimpleBinary()
        {
            Assert.AreEqual("A", Base64BinaryConverter.ConvertBitSetsToBase64(new List<BitSet> { new BitSet(0, 6) }));
            Assert.AreEqual("B", Base64BinaryConverter.ConvertBitSetsToBase64(new List<BitSet> { new BitSet(1, 6) }));
        }
    }
}
