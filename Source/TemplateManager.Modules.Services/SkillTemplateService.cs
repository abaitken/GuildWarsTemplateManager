using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
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

        #region ISkillTemplateService Members

        public void RefreshTemplates(string buildStore)
        {
            buildFactory = NativeBuildFactory.Create(buildStore);
        }

        public TemplateFolder TemplateFolder
        {
            get
            {
                return CreateTemplateFolder(buildFactory.TemplateFolder);
            }
        }

        public IEnumerable<SkillTemplate> AllTemplates
        {
            get
            {
                return FlattenTemplateStructure(TemplateFolder);
            }
        }

        private static IEnumerable<SkillTemplate> FlattenTemplateStructure(TemplateFolder templateFolder)
        {
            var subFolderItem = from subFolder in templateFolder.SubFolders
                                from item in FlattenTemplateStructure(subFolder)
                                select item;
            
            return templateFolder.Templates.Union(subFolderItem);
        }


        private TemplateFolder CreateTemplateFolder(NativeTemplateFolder folder)
        {
            var templates = from nativeTemplate in folder.Templates
                            select CreateTemplate(nativeTemplate.Key, nativeTemplate.Value);

            var subFolders = from nativeFolder in folder.SubFolders
                             select CreateTemplateFolder(nativeFolder);

            return new TemplateFolder(folder.FolderPath, templates, subFolders);
        }

        private SkillTemplate CreateInvalidTemplate(string templatePath)
        {
            return new SkillTemplate(
                templatePath,
                dataService.EmptyProfession);
        }
        // TODO : Consider where skills do not match the primary or secondary profession
        private SkillTemplate CreateTemplate(string templatePath, NativeSkillBuild nativeTemplate)
        {
            if (nativeTemplate == null)
                return CreateInvalidTemplate(templatePath);

            var primaryProfession = dataService.Professions.FirstOrDefault(i => i.TemplateId == nativeTemplate.PrimaryProfessionId);

            if (primaryProfession == null)
                return CreateInvalidTemplate(templatePath);

            var secondaryProfession = dataService.Professions.First(i => i.TemplateId == nativeTemplate.SecondaryProfessionId);

            if (secondaryProfession == null)
                return CreateInvalidTemplate(templatePath);

            return new SkillTemplate(
                templatePath,
                primaryProfession,
                secondaryProfession,
                JoinSkillData(nativeTemplate).ToList(),
                JoinAttributedata(nativeTemplate));
        }

        #endregion

        private IEnumerable<AttributeValue> JoinAttributedata(NativeSkillBuild nativeItem)
        {
            // TODO : This will throw if the attribute does not exist
            return
                from nativeAttribute in nativeItem.AttributeIdValuePairs
                let attribute = dataService.Attributes.First(i => i.TemplateId == nativeAttribute.Key)
                select new AttributeValue(attribute, nativeAttribute.Value);
        }

        private IEnumerable<ISkill> JoinSkillData(NativeSkillBuild nativeItem)
        {
            return
                from nativeSkill in nativeItem.SkillIds
                join skill in dataService.Skills
                    on nativeSkill equals skill.TemplateId
                    into tempSkills
                from result in tempSkills.DefaultIfEmpty(dataService.InvalidSkill)
                select result;
        }
    }

}