using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TemplateManager.DataFetcher.DataProviders;
using TemplateManager.DataFetcher.Logging;
using TemplateManager.DataFetcher.Parsers;

namespace TemplateManager.DataFetcher.Tests
{
    [TestClass]
    public class TranslationProviderTests
    {
        private readonly IEnumerable<string> pages = new[]
                                                         {
                                                             "Localized warrior skill names",
                                                             "Localized ranger skill names",
                                                             "Localized monk skill names",
                                                             "Localized necromancer skill names",
                                                             "Localized mesmer skill names",
                                                             "Localized elementalist skill names",
                                                             "Localized assassin skill names",
                                                             "Localized ritualist skill names",
                                                             "Localized paragon skill names",
                                                             "Localized dervish skill names"
                                                         };

        private IDataProvider dataProvider;
        private ILogger logger;

        private Mock<IDataProvider> mockedDataProvider;
        private Mock<ILogger> mockedLogger;

        [TestInitialize]
        public void Initialize()
        {
            mockedDataProvider = new Mock<IDataProvider>();

            foreach (var page in pages)
                mockedDataProvider.Setup(o => o.RequestData(page, true)).Returns(LoadFile(page)).AtMostOnce();

            mockedLogger = new Mock<ILogger>();
            mockedLogger.Setup(o => o.Log(It.IsAny<Type>(), It.IsAny<string>(), It.IsAny<LogSeverity>()));

            logger = mockedLogger.Object;
            dataProvider = mockedDataProvider.Object;
        }

        [TestMethod]
        [DeploymentItem("LocalizedArticles", "LocalizedArticles")]
        public void CorrectlyBuildsAndFetches()
        {
            var target = new TranslationProvider(dataProvider, logger);
            target.Build();

            var translations = target.FetchSkillName("rush", "Rush");

            Assert.AreEqual(10, translations.Count);
            mockedDataProvider.VerifyAll();
        }

        [TestMethod]
        [DeploymentItem("LocalizedArticles", "LocalizedArticles")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SubsequentBuildsFail()
        {
            var target = new TranslationProvider(dataProvider, logger);
            target.Build();
            target.Build();
        }

        [TestMethod]
        [DeploymentItem("LocalizedArticles", "LocalizedArticles")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BuildMustBeCalledBeforeFetch()
        {
            var target = new TranslationProvider(dataProvider, logger);
            target.FetchSkillName("b", "a");
        }


        private static string LoadFile(string page)
        {
            var currentDirectory = Environment.CurrentDirectory;
            var stringsFolder = Path.Combine(currentDirectory, "LocalizedArticles");
            var filePath = Path.Combine(stringsFolder, string.Format("{0}.txt", page));

            var result = File.ReadAllText(filePath);
            return result;
        }
    }
}