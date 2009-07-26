using System.Collections.Generic;
using System.Windows;

namespace TemplateManager.Assets
{
    public class NativeData : FrameworkElement
    {
        private readonly List<NativeSkillAttribute> attributes;
        private readonly List<NativeProfession> professions;
        private readonly List<NativeSkill> skills;
        private readonly List<NativeSkillType> skillTypes;
        private readonly List<NativeCampaign> campaigns;

        public NativeData()
        {
            skills = new List<NativeSkill>();
            skillTypes = new List<NativeSkillType>();
            campaigns = new List<NativeCampaign>();
            professions = new List<NativeProfession>();
            attributes = new List<NativeSkillAttribute>();
        }

        public List<NativeCampaign> Campaigns
        {
            get { return campaigns; }
        }

        public List<NativeSkillType> SkillTypes
        {
            get { return skillTypes; }
        }

        public List<NativeSkill> Skills
        {
            get { return skills; }
        }

        public List<NativeSkillAttribute> Attributes
        {
            get { return attributes; }
        }

        public List<NativeProfession> Professions
        {
            get { return professions; }
        }
    }
}