using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TemplateManager.AssetBuilder.Tests
{
    [TestClass]
    public class XamlBuilderTests
    {
        [TestMethod]
        public void CanCreateValidImageXaml()
        {
            var result = XamlBuilder.CreateImageXaml(new KeyValuePair<string, BitmapImage>("SomeImage.png", new BitmapImage()));

            Assert.IsTrue(result.Contains(@"x:Key=""SomeImage"""), result);
        }

    }
}
