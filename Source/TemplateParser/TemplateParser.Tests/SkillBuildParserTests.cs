using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateParser.Skills;

namespace TemplateParser.Tests
{
    internal static class TestExtensions
    {
        public static bool ContainsPair<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source,
                                                      TKey key,
                                                      TValue value)
        {
            return source.Contains(new KeyValuePair<TKey, TValue>(key, value));
        }
    }

    [TestClass]
    public class SkillBuildParserTests
    {
        // TODO : write a test to handle the situation where the skill index does not exist

        [TestMethod]
        public void CanParseNewStyleBuildCode()
        {
            const string templateCode = "OwAT043A5JjsR0I3tp/m2mITAA";

            var parser = new SkillBuildParser();

            var build = parser.ParseTemplateCode(templateCode);

            Assert.AreEqual((int) ProfessionIndex.Monk, build.PrimaryProfessionId);
            Assert.AreEqual((int) ProfessionIndex.None, build.SecondaryProfessionId);

            Assert.AreEqual(3, build.AttributeIdValuePairs.Count());
            Assert.IsTrue(build.AttributeIdValuePairs.ContainsPair((int) AttributeIndex.ProtectionPrayers, 3));
            Assert.IsTrue(build.AttributeIdValuePairs.ContainsPair((int) AttributeIndex.DivineFavor, 12));
            Assert.IsTrue(build.AttributeIdValuePairs.ContainsPair((int) AttributeIndex.HealingPrayers, 12));

            Assert.AreEqual(8, build.SkillIds.Count());
            Assert.IsTrue(build.SkillIds.Contains((int) SkillIndex.OrisonofHealing));
            Assert.IsTrue(build.SkillIds.Contains((int) SkillIndex.DwaynasKiss));
            Assert.IsTrue(build.SkillIds.Contains((int) SkillIndex.WordofHealing));
            Assert.IsTrue(build.SkillIds.Contains((int) SkillIndex.SignetofRejuvenation));
            Assert.IsTrue(build.SkillIds.Contains((int) SkillIndex.CureHex));
            Assert.IsTrue(build.SkillIds.Contains((int) SkillIndex.DismissCondition));
            Assert.IsTrue(build.SkillIds.Contains((int) SkillIndex.DivineSpirit));
            Assert.IsTrue(build.SkillIds.Contains((int) SkillIndex.Rebirth));
        }

        [TestMethod]
        public void CanCreateBuildTemplate()
        {
            const string expected = "OwAT043A5JjsR0I3tp/m2mITAA";

            var build = new NativeSkillBuild
                            {
                                PrimaryProfessionId = (int) ProfessionIndex.Monk,
                                SecondaryProfessionId = (int) ProfessionIndex.None
                            };

            build.AddAttribute((int) AttributeIndex.HealingPrayers, 12);
            build.AddAttribute((int) AttributeIndex.ProtectionPrayers, 3);
            build.AddAttribute((int) AttributeIndex.DivineFavor, 12);

            build.AddSkill((int) SkillIndex.OrisonofHealing);
            build.AddSkill((int) SkillIndex.DwaynasKiss);
            build.AddSkill((int) SkillIndex.WordofHealing);
            build.AddSkill((int) SkillIndex.SignetofRejuvenation);
            build.AddSkill((int) SkillIndex.CureHex);
            build.AddSkill((int) SkillIndex.DismissCondition);
            build.AddSkill((int) SkillIndex.DivineSpirit);
            build.AddSkill((int) SkillIndex.Rebirth);


            var parser = new SkillBuildParser();
            var result = parser.CreateTemplateCode(build);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CanParseOldStyleBuildCode()
        {
            const string templateCode = "ABJRkncAAAAAAAAAAAAA";

            var parser = new SkillBuildParser();
            var build = parser.ParseTemplateCode(templateCode);

            Assert.AreEqual((int) ProfessionIndex.Warrior, build.PrimaryProfessionId);
            Assert.AreEqual((int) ProfessionIndex.Necromancer, build.SecondaryProfessionId);

            Assert.AreEqual(2, build.AttributeIdValuePairs.Count());
            Assert.IsTrue(build.AttributeIdValuePairs.ContainsPair((int) AttributeIndex.Strength, 12));
            Assert.IsTrue(build.AttributeIdValuePairs.ContainsPair((int) AttributeIndex.HammerMastery, 12));

            Assert.AreEqual(8, build.SkillIds.Count());

            /* 8 x NoSkill */
            Assert.AreEqual(0, build.SkillIds.First());
            Assert.AreEqual(0, build.SkillIds.Skip(1).First());
            Assert.AreEqual(0, build.SkillIds.Skip(2).First());
            Assert.AreEqual(0, build.SkillIds.Skip(3).First());
            Assert.AreEqual(0, build.SkillIds.Skip(4).First());
            Assert.AreEqual(0, build.SkillIds.Skip(5).First());
            Assert.AreEqual(0, build.SkillIds.Skip(6).First());
            Assert.AreEqual(0, build.SkillIds.Skip(7).First());
        }


        [TestMethod]
        public void WillNotParseInvalidTemplates()
        {
            const string templateCode = "this will not parse";

            var parser = new SkillBuildParser();
            var build = parser.ParseTemplateCode(templateCode);

            Assert.AreSame(null, build);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotParseBuildWithLessThanEightSkills()
        {
            var build = new NativeSkillBuild
                            {
                                PrimaryProfessionId = (int) ProfessionIndex.Dervish,
                                SecondaryProfessionId = (int) ProfessionIndex.None
                            };

            var parser = new SkillBuildParser();
            parser.CreateTemplateCode(build);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotParseBuildWithoutPrimaryProfession()
        {
            var build = new NativeSkillBuild();

            build.AddSkill((int) SkillIndex.OrisonofHealing);
            build.AddSkill((int) SkillIndex.DwaynasKiss);
            build.AddSkill((int) SkillIndex.WordofHealing);
            build.AddSkill((int) SkillIndex.SignetofRejuvenation);
            build.AddSkill((int) SkillIndex.CureHex);
            build.AddSkill((int) SkillIndex.DismissCondition);
            build.AddSkill((int) SkillIndex.DivineSpirit);
            build.AddSkill((int) SkillIndex.Rebirth);

            var parser = new SkillBuildParser();
            parser.CreateTemplateCode(build);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotParseBuildWithMatchingProfessions()
        {
            var build = new NativeSkillBuild
                            {
                                PrimaryProfessionId = (int) ProfessionIndex.Monk,
                                SecondaryProfessionId = (int) ProfessionIndex.Monk
                            };

            build.AddSkill((int) SkillIndex.OrisonofHealing);
            build.AddSkill((int) SkillIndex.DwaynasKiss);
            build.AddSkill((int) SkillIndex.WordofHealing);
            build.AddSkill((int) SkillIndex.SignetofRejuvenation);
            build.AddSkill((int) SkillIndex.CureHex);
            build.AddSkill((int) SkillIndex.DismissCondition);
            build.AddSkill((int) SkillIndex.DivineSpirit);
            build.AddSkill((int) SkillIndex.Rebirth);

            var parser = new SkillBuildParser();
            parser.CreateTemplateCode(build);
        }

        [TestMethod]
        public void CannotParseInvalidBase64String()
        {
            var parser = new SkillBuildParser();
            var result = parser.ParseTemplateCode("pad!£$%^&*()_+-={}[]:@~;'#<>?,./");

            Assert.IsNull(result);
        }
    }
}