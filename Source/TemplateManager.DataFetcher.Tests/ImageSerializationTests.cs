using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateManager.Common;

namespace TemplateManager.DataFetcher.Tests
{
    [TestClass]
    public class ImageSerializationTests
    {
        [TestMethod]
        [DeploymentItem("1.jpg")]
        public void WillDeSerializeCorrectly()
        {
            var sourceBytes = ImageSerializer.GetImage("1.jpg");
            var result = ImageSerializer.CreateImage(sourceBytes);
        }
    }
}