using System.Collections.Generic;
using InfiniteRain.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TemplateManager.Common.Tests
{
    [TestClass]
    public class CommandLineParserTests
    {
        private readonly string[] enclosingchars = new[]
                                                       {
                                                           "", @"""", "'"
                                                       };

        private readonly string[] prefixes = new[]
                                                 {
                                                     "-",
                                                     "/",
                                                     "--"
                                                 };

        private readonly string[] separators = new[]
                                                   {
                                                       " ",
                                                       "=",
                                                       ":"
                                                   };


        private static IEnumerable<string> CreateArgs(string name,
                                                      object value,
                                                      string prefix,
                                                      string separator,
                                                      string enclosing)
        {
            if(string.IsNullOrEmpty(separator.Trim()))
            {
                yield return string.Format("{0}{1}", prefix, name);
                yield return string.Format("{0}{1}{0}", enclosing, value);
                yield break;
            }

            yield return string.Format("{0}{3}{1}{2}{4}{2}", prefix, separator, enclosing, name, value);
        }

        private static IEnumerable<string> CreateArgs(string name, string prefix)
        {
            yield return string.Format("{0}{1}", prefix, name);
        }

        [TestMethod]
        public void Test()
        {
            foreach(var prefix in prefixes)
            {
                TestSingleItem(prefix);

                foreach(var separator in separators)
                {
                    foreach(var enclosing in enclosingchars)
                    {
                        TestValuePair(prefix, separator, enclosing);
                    }
                }
            }
        }

        private static void TestValuePair(string prefix, string separator, string enclosing)
        {
            const string name = "param";
            const string value = "value";

            var testItems = CreateArgs(name, value, prefix, separator, enclosing);
            var result = CommandLineParser.Parse(testItems);

            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result[name]);
            Assert.AreEqual(value, result.GetValue(name));
        }

        private static void TestSingleItem(string prefix)
        {
            const string name = "param";
            var testItems = CreateArgs(name, prefix);
            var result = CommandLineParser.Parse(testItems);

            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result[name]);
        }
    }
}