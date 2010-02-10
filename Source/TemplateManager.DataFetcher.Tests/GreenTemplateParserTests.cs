using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateManager.DataFetcher.Parsers;

namespace TemplateManager.DataFetcher.Tests
{
    [TestClass]
    public class GreenTemplateParserTests
    {
        [TestMethod]
        public void ParseTest()
        {
            var testItems = new[]
                                {
                                    new[]
                                        {
                                            "padding 82...154...172 padding", "padding {{gr|82|172}} padding"
                                        },
                                    new[]
                                        {
                                            "+5...17...20", "{{gr|+5|20}}"
                                        },
                                    new[]
                                        {
                                            "20...87...104", "{{Gr|20|104}}"
                                        },
                                    new[]
                                        {
                                            "20...87...104", "{{Gr|20|104|}}"
                                        },
                                    new[]
                                        {
                                            "20...104", "{{Gr2|20|104}}"
                                        },
                                    new[]
                                        {
                                            "20%...87%...104%", "{{Gr|20%|104%|}}"
                                        },
                                    new[]
                                        {
                                            "20%...104%", "{{Gr2|20%|104%}}"
                                        }
                                };

            foreach(var item in testItems)
            {
                Assert.AreEqual(item[0], GreenTemplateParser.Parse(item[1]));
            }
        }
    }
}