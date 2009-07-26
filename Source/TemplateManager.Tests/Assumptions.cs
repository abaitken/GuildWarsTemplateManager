using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TemplateManager.Tests
{
    [TestClass]
    public class Assumptions
    {
        [TestMethod]
        public void IndexOfFailure()
        {
            const string source = "source";
            const string other = "other";

            Assert.AreEqual(-1, CultureInfo.CurrentUICulture.CompareInfo.IndexOf(source, other, CompareOptions.IgnoreCase));
        }
    }
}