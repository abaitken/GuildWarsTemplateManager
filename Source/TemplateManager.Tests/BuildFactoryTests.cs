﻿using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TemplateManager.Infrastructure;
using TemplateManager.Modules.Services;

namespace TemplateManager.Tests
{
    [TestClass]
    public class BuildFactoryTests
    {
        [TestMethod]
        [DeploymentItem("SimpleTest", "SimpleTest")]
        [DeploymentItem("TemplateManager.Data.xml")]
        public void CanGetRelationalBuildData()
        {
            var mockedAppSettings = new Mock<IApplicationSettings>();

            var buildPath = Path.Combine(Environment.CurrentDirectory, "SimpleTest");

            var factory = new SkillTemplateService(new DataService(), mockedAppSettings.Object);
            factory.RefreshTemplates(buildPath);
            Assert.AreEqual(1, factory.TemplateFolder.Templates.Count());

            var build = factory.TemplateFolder.Templates.First();

            Assert.AreEqual("Logaan", build.Author);
            Assert.AreEqual("group disrupting", build.Tags);
            Assert.AreEqual("This is a note", build.Notes);
        }
    }
}