using System;
using System.Linq;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateManager.Modules.Services.NativeObjects;

namespace TemplateManager.Tests
{
    [TestClass]
    public class NativeBuildFactoryTests
    {
        [TestMethod]
        [DeploymentItem("TestBuilds", "TestBuilds")]
        public void CanLoadBuildsFromDisk()
        {
            var buildPath = Path.Combine(Environment.CurrentDirectory, "TestBuilds");

            var templateFolder = new NativeBuildFactory(buildPath).TemplateFolder;
            
            
            Assert.AreEqual(2, templateFolder.SubFolders.Count());
            Assert.AreEqual(1, templateFolder.SubFolders.First().Templates.Count());
            Assert.AreEqual(1, templateFolder.SubFolders.Skip(1).First().Templates.Count());
            Assert.AreEqual(1, templateFolder.Templates.Count());
        }
    }
}