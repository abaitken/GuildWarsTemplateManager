using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using TemplateManager.DataFetcher.Logging;
using TemplateManager.DataFetcher.Model;

namespace TemplateManager.DataFetcher.DataTargets
{
    internal class TemplateManagerTarget : IDataTarget
    {
        private const string dataFile = "TemplateManager.Data.GuildWars.xml";
        private readonly ILogger logger;
        private readonly Data.GuildWars.Model model;

        public TemplateManagerTarget(ILogger logger)
        {
            this.logger = logger;
            model = new Data.GuildWars.Model();
        }

        #region IDataTarget Members

        public void Update(IEnumerable<Skill> data)
        {
            logger.Log(GetType(), "Building data structures", LogSeverity.InformationHigh);

            if(File.Exists(dataFile))
            {
                model.ReadXml(dataFile);
                model.AcceptChanges();
            }

            UpdateRemoved(data);
            UpdateCauses(data);
            UpdateCategories(data);
            UpdateProfessions(data);
            UpdateAttributes(data);
            UpdateProjectiles(data);
            UpdateTargets(data);
            UpdateSpecialTypes(data);
            UpdateSkillTypes(data);
            UpdateCampaigns(data);
            UpdateAreaOfEffects(data);
            UpdateRanges(data);

            logger.Log(GetType(), "Writing data to disk", LogSeverity.InformationHigh);
            model.WriteXml(dataFile, XmlWriteMode.IgnoreSchema);

            logger.Log(GetType(), "Update complete", LogSeverity.InformationHigh);
        }

        #endregion

        private void UpdateRanges(IEnumerable<Skill> enumerable)
        {
            UpdateTable<Data.GuildWars.Model.RangesDataTable, Data.GuildWars.Model.RangesRow>(
                enumerable,
                i => i.Range,
                i => i.Ranges,
                i => i.Name,
                v => model.Ranges.AddRangesRow(v));
        }

        private void UpdateSkillTypes(IEnumerable<Skill> enumerable)
        {
            UpdateTable<Data.GuildWars.Model.SkillTypesDataTable, Data.GuildWars.Model.SkillTypesRow>(
                enumerable,
                i => i.Type,
                i => i.SkillTypes,
                i => i.Name,
                AddSkillType);
        }

        private void AddSkillType(string obj)
        {
            var row = model.SkillTypes.NewSkillTypesRow();
            row.Name = obj;
            model.SkillTypes.AddSkillTypesRow(row);
        }

        private void UpdateCampaigns(IEnumerable<Skill> enumerable)
        {
            UpdateTable<Data.GuildWars.Model.CampaignsDataTable, Data.GuildWars.Model.CampaignsRow>(
                enumerable,
                i => i.Campaign,
                i => i.Campaigns,
                i => i.Name,
                v => model.Campaigns.AddCampaignsRow(v));
        }

        private void UpdateAreaOfEffects(IEnumerable<Skill> enumerable)
        {
            UpdateTable<Data.GuildWars.Model.AreaOfEffectsDataTable, Data.GuildWars.Model.AreaOfEffectsRow>(
                enumerable,
                i => i.AreaOfEffect,
                i => i.AreaOfEffects,
                i => i.Name,
                v => model.AreaOfEffects.AddAreaOfEffectsRow(v));
        }

        private void UpdateSpecialTypes(IEnumerable<Skill> enumerable)
        {
            UpdateTable<Data.GuildWars.Model.SpecialTypesDataTable, Data.GuildWars.Model.SpecialTypesRow>(
                enumerable,
                i => i.SpecialType,
                i => i.SpecialTypes,
                i => i.Name,
                v => model.SpecialTypes.AddSpecialTypesRow(v));
        }

        private void UpdateTargets(IEnumerable<Skill> enumerable)
        {
            UpdateTable<Data.GuildWars.Model.TargetsDataTable, Data.GuildWars.Model.TargetsRow>(
                enumerable,
                i => i.Target,
                i => i.Targets,
                i => i.Name,
                v => model.Targets.AddTargetsRow(v));
        }

        private void UpdateProjectiles(IEnumerable<Skill> enumerable)
        {
            UpdateTable<Data.GuildWars.Model.ProjectilesDataTable, Data.GuildWars.Model.ProjectilesRow>(
                enumerable,
                i => i.Projectile,
                i => i.Projectiles,
                i => i.Name,
                v => model.Projectiles.AddProjectilesRow(v));
        }

        private void UpdateProfessions(IEnumerable<Skill> enumerable)
        {
            UpdateTable<Data.GuildWars.Model.ProfessionsDataTable, Data.GuildWars.Model.ProfessionsRow>(
                enumerable,
                i => i.Profession,
                i => i.Professions,
                i => i.Name,
                AddProfessionsRow);
        }

        private void AddProfessionsRow(string searchValue)
        {
            var row = model.Professions.NewProfessionsRow();

            row.Name = searchValue;

            model.Professions.AddProfessionsRow(row);
        }

        private void UpdateAttributes(IEnumerable<Skill> enumerable)
        {
            UpdateTable<Data.GuildWars.Model.AttributesDataTable, Data.GuildWars.Model.AttributesRow>(
                enumerable,
                i => i.Attribute,
                i => i.Attributes,
                i => i.Name,
                AddCategoriesRow);
        }

        private void AddCategoriesRow(string searchValue)
        {
            var row = model.Attributes.NewAttributesRow();

            row.Name = searchValue;

            model.Attributes.AddAttributesRow(row);
        }

        private void UpdateCategories(IEnumerable<Skill> enumerable)
        {
            UpdateTable<Data.GuildWars.Model.CategoriesDataTable, Data.GuildWars.Model.CategoriesRow>(
                enumerable,
                i => i.Categories,
                i => i.Categories,
                i => i.Name,
                v => model.Categories.AddCategoriesRow(v));
        }

        private void UpdateTable<T1, T2>(IEnumerable<Skill> skills,
                                         Func<Skill, string> itemSelector,
                                         Func<Data.GuildWars.Model, T1> modelSelector,
                                         Func<T2, string> propertySelector,
                                         Action<string> addAction)
            where T1 : IEnumerable<T2>
            where T2 : DataRow
        {
            var groupedValuesQuery = from item in skills
                                     let innerItem = itemSelector(item)
                                     where innerItem != null
                                     group innerItem by innerItem.ToLower()
                                     into g
                                         select g.Key;

            var groupedValues = groupedValuesQuery.ToList();

            UpdateTable(groupedValues, modelSelector, propertySelector, addAction);
        }

        private void UpdateTable<T1, T2>(IEnumerable<string> groupedValues,
                                         Func<Data.GuildWars.Model, T1> modelSelector,
                                         Func<T2, string> propertySelector,
                                         Action<string> addAction)
            where T1 : IEnumerable<T2>
            where T2 : DataRow
        {
            foreach(var value in groupedValues)
            {
                var searchValue = value;
                var existingItem =
                    modelSelector(model).FirstOrDefault(
                        i => propertySelector(i).Equals(searchValue, StringComparison.InvariantCultureIgnoreCase));

                if(existingItem != null)
                {
                    existingItem.SetModified();
                    continue;
                }

                addAction(searchValue);
            }

            foreach(var value in modelSelector(model))
            {
                if(value.RowState == DataRowState.Unchanged)
                    value.Delete();
            }
        }

        private void UpdateTable<T1, T2>(IEnumerable<Skill> skills,
                                         Func<Skill, IEnumerable<string>> tableSelector,
                                         Func<Data.GuildWars.Model, T1> modelSelector,
                                         Func<T2, string> propertySelector,
                                         Action<string> addAction) where T1 : IEnumerable<T2> where T2 : DataRow
        {
            var groupedValuesQuery = from item in skills
                                     let items = tableSelector(item)
                                     where items != null
                                     from innerItem in items
                                     group innerItem by innerItem.ToLower()
                                     into g
                                         select g.Key;

            var groupedValues = groupedValuesQuery.ToList();

            UpdateTable(groupedValues, modelSelector, propertySelector, addAction);
        }

        private void UpdateCauses(IEnumerable<Skill> enumerable)
        {
            UpdateTable<Data.GuildWars.Model.CausesDataTable, Data.GuildWars.Model.CausesRow>(
                enumerable,
                i => i.Causes,
                i => i.Causes,
                i => i.Name,
                v => model.Causes.AddCausesRow(v));
        }

        private void UpdateRemoved(IEnumerable<Skill> enumerable)
        {
            UpdateTable<Data.GuildWars.Model.RemovesDataTable, Data.GuildWars.Model.RemovesRow>(
                enumerable,
                i => i.Removes,
                i => i.Removes,
                i => i.Name,
                v => model.Removes.AddRemovesRow(v));
        }

        private static byte[] GetImage(Image image)
        {
            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        private static byte[] GetImage(string imageName)
        {
            var image = Image.FromFile(Path.Combine("images", imageName));
            return GetImage(image);
        }
    }
}