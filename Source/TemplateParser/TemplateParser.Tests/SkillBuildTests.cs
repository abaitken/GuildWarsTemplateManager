using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateParser.Skills;

namespace TemplateParser.Tests
{
    [TestClass]
    public class SkillBuildTests
    {
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void CannotAddDuplicateAttrbutes()
        {
            var build = new NativeSkillBuild();

            build.AddAttribute((int)AttributeIndex.AirMagic, 5);
            build.AddAttribute((int)AttributeIndex.AirMagic, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotAddMoreThanEightSkills()
        {
            var build = new NativeSkillBuild();

            build.AddSkill((int)SkillIndex.AbaddonsChosen);
            build.AddSkill((int)SkillIndex.AbaddonsConspiracy);
            build.AddSkill((int)SkillIndex.AbaddonsFavor);
            build.AddSkill((int)SkillIndex.AccumulatedPain);
            build.AddSkill((int)SkillIndex.AcidTrap);
            build.AddSkill((int)SkillIndex.AdventurersInsight);
            build.AddSkill((int)SkillIndex.Aegis);
            build.AddSkill((int)SkillIndex.AegisPvP);
            build.AddSkill((int)SkillIndex.AfflictedSoulExplosion);
            build.AddSkill((int)SkillIndex.Aftershock);
        }

        [TestMethod]
        public void CannotAddDuplicateSkills()
        {
            var build = new NativeSkillBuild();

            build.AddSkill((int)SkillIndex.AbaddonsChosen);
            build.AddSkill((int)SkillIndex.AbaddonsChosen);

            Assert.AreEqual(1, build.SkillIds.Count());
        }
    }
}
