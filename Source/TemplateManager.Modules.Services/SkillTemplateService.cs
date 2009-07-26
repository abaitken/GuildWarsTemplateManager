using System.Collections.Generic;
using System.Linq;
using TemplateManager.Infrastructure.Model;
using TemplateManager.Infrastructure.Services;
using TemplateManager.Modules.Services.NativeObjects;
using TemplateParser.Skills;

namespace TemplateManager.Modules.Services
{
    internal class SkillTemplateService : ISkillTemplateService
    {
        private readonly IDataService dataService;
        private NativeBuildFactory buildFactory;

        public SkillTemplateService(IDataService dataService)
        {
            this.dataService = dataService;
            buildFactory = NativeBuildFactory.Empty;
        }

        private Skill UnknownSkill
        {
            get { return dataService.Skills.First(i => i.NativeId == -1); }
        }

        #region ISkillTemplateService Members

        public void RefreshTemplates(string buildStore)
        {
            buildFactory = NativeBuildFactory.Create(buildStore);
        }

        public IList<SkillTemplate> Templates
        {
            get
            {
                var validBuilds =
                    from buildPair in buildFactory.Builds
                    let nativeItem = buildPair.Value
                    where nativeItem != null
                    select new SkillTemplate(
                        buildPair.Key,
                        dataService.Professions.First(i => i.NativeId == nativeItem.PrimaryProfessionId),
                        dataService.Professions.First(i => i.NativeId == nativeItem.SecondaryProfessionId),
                        JoinSkillData(nativeItem).ToList(),
                        JoinAttributedata(nativeItem));

                var invalidBuilds =
                    from buildPair in buildFactory.Builds
                    let nativeItem = buildPair.Value
                    where nativeItem == null
                    select new SkillTemplate(
                        buildPair.Key,
                        dataService.EmptyProfession);

                return validBuilds.Union(invalidBuilds).ToList();
            }
        }

        #endregion

        private IEnumerable<AttributeValue> JoinAttributedata(NativeSkillBuild nativeItem)
        {
            // TODO : This will throw if the attribute does not exist
            return
                from nativeAttribute in nativeItem.AttributeIdValuePairs
                let attribute = dataService.Attributes.First(i => i.NativeId == nativeAttribute.Key)
                select new AttributeValue(attribute, nativeAttribute.Value);
        }

        private IEnumerable<Skill> JoinSkillData(NativeSkillBuild nativeItem)
        {
            return
                from nativeSkill in nativeItem.SkillIds
                join skill in dataService.Skills
                    on nativeSkill equals skill.NativeId
                    into tempSkills
                from result in tempSkills.DefaultIfEmpty(UnknownSkill)
                select result;
        }
    }
}