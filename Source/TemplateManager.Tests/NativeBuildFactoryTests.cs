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

            var factory = new NativeBuildFactory(buildPath);
            Assert.AreEqual(3, factory.Builds.Count());
        }
    }
}