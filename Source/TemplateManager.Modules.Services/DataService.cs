using System.Collections.Generic;
using System.Linq;
using TemplateManager.Assets;
using TemplateManager.Infrastructure.Model;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.Modules.Services
{
    internal class DataService : IDataService
    {
        public DataService()
        {
            var assetLoader = new AssetLoader();
            var data = assetLoader.Load();

            Professions = GetProfessions(data).ToList();
            Skills = GetSkills(data).ToList();
            Attributes = GetAttributes(data).ToList();
        }

        #region IDataService Members

        public IEnumerable<Profession> Professions { get; private set; }

        public IEnumerable<Profession> PrimaryProfessions
        {
            get
            {
                return from profession in Professions
                       where profession.ValidPrimary
                       orderby profession.Name
                       select profession;
            }
        }

        public IEnumerable<Profession> SecondaryProfessions
        {
            get
            {
                return from profession in Professions
                       where profession.ValidSecondary
                       orderby profession.Name
                       select profession;
            }
        }

        public Profession EmptyProfession
        {
            get { return Professions.First(i => i.NativeId == 0); }
        }

        public IEnumerable<Skill> Skills { get; private set; }

        public IEnumerable<SkillAttribute> Attributes { get; private set; }

        #endregion

        private static IEnumerable<SkillAttribute> GetAttributes(NativeData factory)
        {
            return from nativeItem in factory.Attributes
                   select new SkillAttribute(nativeItem.Name, nativeItem.AttributeId, nativeItem.IsPrimary);
        }

        private static IEnumerable<Skill> GetSkills(NativeData factory)
        {
            return from nativeItem in factory.Skills
                   select
                       new Skill(nativeItem.SkillId, nativeItem.Name, nativeItem.Image, nativeItem.Description);
        }

        private static IEnumerable<Profession> GetProfessions(NativeData factory)
        {
            return from nativeItem in factory.Professions
                   select
                       new Profession(nativeItem.ProfessionId,
                                      nativeItem.Name,
                                      nativeItem.Image,
                                      nativeItem.ValidPrimary,
                                      nativeItem.ValidSecondary);
        }
    }
}