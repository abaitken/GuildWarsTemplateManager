using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateManager.Assets;

namespace TemplateManager.Tests
{
    [TestClass]
    public class AssetTests
    {
        [TestMethod]
        public void CanLoadAssetData()
        {
            var resourceStream =
                Assembly.Load(new AssemblyName("TemplateManager.Assets")).GetManifestResourceStream(
                    "TemplateManager.Assets.Data.xaml");

            var reader = new StreamReader(resourceStream);
            reader.ReadToEnd();

            var assetLoader = new AssetLoader();
            assetLoader.Load();
        }
    }
}