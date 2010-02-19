using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TemplateManager.DataFetcher.Abstractions;
using TemplateManager.DataFetcher.DataProviders;
using TemplateManager.DataFetcher.Logging;
using TemplateManager.DataFetcher.Parsers;

namespace TemplateManager.DataFetcher.Tests
{
    [TestClass]
    public class ParserTests
    {
        private IDataProvider dataProvider;
        private IFileSystemAbstraction fileSystem;
        private ILogger logger;

        private Mock<IDataProvider> mockedDataProvider;
        private Mock<IFileSystemAbstraction> mockedFileSystem;
        private Mock<ILogger> mockedLogger;
        private IParser target;

        [TestInitialize]
        public void Initialize()
        {
            mockedDataProvider = new Mock<IDataProvider>();

            mockedLogger = new Mock<ILogger>();
            mockedLogger.Setup(o => o.Log(It.IsAny<Type>(), It.IsAny<string>(), It.IsAny<LogSeverity>()));

            mockedFileSystem = new Mock<IFileSystemAbstraction>();

            logger = mockedLogger.Object;
            dataProvider = mockedDataProvider.Object;
            fileSystem = mockedFileSystem.Object;

            target = new Parser(logger, dataProvider, fileSystem, ".");
        }

        [TestMethod]
        public void NoGameLinkArticle()
        {
            mockedDataProvider.Setup(o => o.RequestData("Game_link:Skill_1", true)).Returns(() => null);

            var result = target.Fetch(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.SkillId);
            Assert.IsTrue(result.NotASkill);
        }

        [TestMethod]
        public void GameLinkArticleDoesNotContainRedirect()
        {
            mockedDataProvider.Setup(o => o.RequestData("Game_link:Skill_1", true)).Returns(() => "junk");

            var result = target.Fetch(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.SkillId);
            Assert.IsTrue(string.IsNullOrEmpty(result.BasicName));
            Assert.IsTrue(result.NotASkill);
        }

        [TestMethod]
        public void FullArticleNotFound()
        {
            mockedDataProvider.Setup(o => o.RequestData("Game_link:Skill_1", true)).Returns(() => "#REDIRECT [[self:Some Skill]]");
            mockedDataProvider.Setup(o => o.RequestData("Some Skill", true)).Returns(() => null);
            mockedDataProvider.Setup(o => o.RequestData("Some Skill (PvP)", true)).Returns(() => null);

            var result = target.Fetch(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.SkillId);
            Assert.AreEqual("Some_Skill", result.BasicName);
            Assert.IsTrue(result.NotASkill);
        }

        [TestMethod]
        public void NoInfoBoxFound()
        {
            mockedDataProvider.Setup(o => o.RequestData("Game_link:Skill_1", true)).Returns(() => "#REDIRECT [[self:Some Skill]]");
            mockedDataProvider.Setup(o => o.RequestData("Some Skill", true)).Returns(() => "junk");
            mockedDataProvider.Setup(o => o.RequestData("Some Skill (PvP)", true)).Returns(() => null);

            var result = target.Fetch(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.SkillId);
            Assert.AreEqual("Some_Skill", result.BasicName);
            Assert.IsTrue(result.NotASkill);
        }

        [TestMethod]
        [DeploymentItem("ParserTestData", "ParserTestData")]
        public void InfoBoxParsedCorrectly()
        {
            mockedDataProvider.Setup(o => o.RequestData("Game_link:Skill_1", true)).Returns(() => "#REDIRECT [[self:Some Skill]]");
            mockedDataProvider.Setup(o => o.RequestData("Some_Skill", true)).Returns(() => LoadFile("raw.txt"));
            mockedDataProvider.Setup(o => o.RequestData("Some_Skill_(PvP)", true)).Returns(() => null);
            mockedDataProvider.Setup(o => o.RequestData("Some_Skill", false)).Returns(() => null);
            mockedDataProvider.Setup(o => o.CreateUrl("Some_Skill", false)).Returns("httpSome Skill");

            var result = target.Fetch(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.SkillId);
            Assert.AreEqual("Some_Skill", result.BasicName);
            Assert.AreEqual("httpSome Skill", result.WikiLink);
            Assert.AreEqual("Healing Signet", result.Name);
            Assert.AreEqual("Core", result.Campaign);
            Assert.AreEqual("Warrior", result.Profession);
            Assert.AreEqual("Tactics", result.Attribute);
            Assert.AreEqual("Signet", result.Type);
            Assert.AreEqual(2.0, result.ActivationTime.Value);
            Assert.AreEqual(4.0, result.RechargeTime.Value);
            Assert.AreEqual("Signet. You gain 82...154...172 Health. You have -40 armor while using this skill.", result.Description);
            Assert.AreEqual("Signet. You gain 82...154...172 Health. [color=gray]You have -40 armor while using this skill.[/color]", result.ConciseDescription);
        }

        [TestMethod]
        [DeploymentItem("ParserTestData", "ParserTestData")]
        public void WillParseProgression()
        {
            mockedDataProvider.Setup(o => o.RequestData("Game_link:Skill_1", true)).Returns(() => "#REDIRECT [[self:Some Skill]]");
            mockedDataProvider.Setup(o => o.RequestData("Some_Skill", true)).Returns(() => LoadFile("raw.txt"));
            mockedDataProvider.Setup(o => o.RequestData("Some_Skill_(PvP)", true)).Returns(() => null);
            mockedDataProvider.Setup(o => o.RequestData("Some_Skill", false)).Returns(() => null);
            mockedDataProvider.Setup(o => o.CreateUrl("Some_Skill", false)).Returns("httpSome Skill");

            var result = target.Fetch(1);

            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Progression);

            Assert.AreEqual("Tactics", result.Progression.Attribute);
            Assert.IsNotNull(result.Progression.Vars);
            Assert.AreEqual(1, result.Progression.Vars.Count());

            var var = result.Progression.Vars.First();
            Assert.AreEqual("Heal", var.Name);
            Assert.AreEqual("82", var.Level0);
            Assert.AreEqual("172", var.MaxLevel);
        }

        [TestMethod]
        [DeploymentItem("ParserTestData", "ParserTestData")]
        public void WillFetchImageCorrectly()
        {
            mockedDataProvider.Setup(o => o.RequestData("Game_link:Skill_1", true)).Returns(() => "#REDIRECT [[self:Some Skill]]");
            mockedDataProvider.Setup(o => o.RequestData("Some_Skill", true)).Returns(() => LoadFile("raw.txt"));
            mockedDataProvider.Setup(o => o.RequestData("Some_Skill_(PvP)", true)).Returns(() => null);
            mockedDataProvider.Setup(o => o.RequestData("Some_Skill", false)).Returns(() => LoadFile("html.txt"));
            mockedDataProvider.Setup(o => o.CreateUrl("Some_Skill", false)).Returns("http://domain/Some_Skill");
            var image = new Bitmap(1,1);
            mockedDataProvider.Setup(o => o.RequestImage("/images/e/e6/Healing_Signet.jpg")).Returns(() => image).Verifiable();
            
            mockedFileSystem.Setup(o => o.Exists(@".\1.jpg")).Returns(() => false).Verifiable();
            mockedFileSystem.Setup(o => o.Write(@".\1.jpg", image)).Verifiable();

            var result = target.Fetch(1);

            Assert.IsNotNull(result);

            mockedFileSystem.Verify();
            mockedDataProvider.Verify();
        }

        [TestMethod]
        public void FireDartArticle()
        {
            const string skillText = @"{{skill infobox
| id = 2692 <!-- BMP -->
| name = Fire Dart
| campaign = Eye of the North
| type = Skill
| recharge = 5
| special = Environment
| description = [[Skill]]. A flaming projectile flies outward, inflicting 33 damage and setting targets [[Burning|on fire]] for 4 seconds.
| concise description = Skill. Projectile: deals 33 damage and inflicts Burning condition (4 seconds).
}}

==Related Skills==
*{{skill icon|Ice Dart}}
*{{skill icon|Ice Jet}}
*{{skill icon|Ice Spout}}
*{{skill icon|Fire Jet}}
*{{skill icon|Fire Spout}}
*{{skill icon|Poison Dart}}
*{{skill icon|Poison Jet}}
*{{skill icon|Poison Spout}}";

            mockedDataProvider.Setup(o => o.RequestData("Game_link:Skill_1", true)).Returns(() => "#REDIRECT [[self:Some Skill]]");
            mockedDataProvider.Setup(o => o.RequestData("Some Skill", true)).Returns(() => skillText);
            mockedDataProvider.Setup(o => o.RequestData("Some Skill", false)).Returns(() => null);

            var result = target.Fetch(1);
            Assert.IsNotNull(result);
        }

        private static string LoadFile(string page)
        {
            var currentDirectory = Environment.CurrentDirectory;
            var stringsFolder = Path.Combine(currentDirectory, "ParserTestData");
            var filePath = Path.Combine(stringsFolder, string.Format("{0}", page));

            var result = File.ReadAllText(filePath);
            return result;
        }
    }
}