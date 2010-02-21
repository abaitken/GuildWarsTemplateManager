using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TemplateManager.Common;
using TemplateManager.DataFetcher.Logging;
using TemplateManager.DataFetcher.Model;

namespace TemplateManager.DataFetcher.DataTargets
{
    internal class TemplateManagerTarget : IDataTarget
    {
        private const int skillOffset = 2;
        private readonly ILogger logger;
        private Data.GuildWars.Model model;

        public TemplateManagerTarget(ILogger logger)
        {
            this.logger = logger;
        }

        #region IDataTarget Members

        public void Update(IEnumerable<Skill> data)
        {
            PrepareExistingData();

            logger.Log(GetType(), "Building data structures", LogSeverity.InformationHigh);
            logger.Log(GetType(), "... Updating Removes", LogSeverity.InformationHigh);
            UpdateRemoved(data);
            logger.Log(GetType(), "... Updating Causes", LogSeverity.InformationHigh);
            UpdateCauses(data);
            logger.Log(GetType(), "... Updating Categories", LogSeverity.InformationHigh);
            UpdateCategories(data);
            logger.Log(GetType(), "... Updating Professions", LogSeverity.InformationHigh);
            UpdateProfessions(data);
            logger.Log(GetType(), "... Updating Profession images", LogSeverity.InformationHigh);
            UpdateProfessionImages();
            logger.Log(GetType(), "... Updating Attributes", LogSeverity.InformationHigh);
            UpdateAttributes(data);
            logger.Log(GetType(), "... Updating Projectiles", LogSeverity.InformationHigh);
            UpdateProjectiles(data);
            logger.Log(GetType(), "... Updating Targets", LogSeverity.InformationHigh);
            UpdateTargets(data);
            logger.Log(GetType(), "... Updating Special Types", LogSeverity.InformationHigh);
            UpdateSpecialTypes(data);
            logger.Log(GetType(), "... Updating Skill Types", LogSeverity.InformationHigh);
            UpdateSkillTypes(data);
            logger.Log(GetType(), "... Updating Campaigns", LogSeverity.InformationHigh);
            UpdateCampaigns(data);
            logger.Log(GetType(), "... Updating Area of Effects", LogSeverity.InformationHigh);
            UpdateAreaOfEffects(data);
            logger.Log(GetType(), "... Updating Ranges", LogSeverity.InformationHigh);
            UpdateRanges(data);
            logger.Log(GetType(), "... Updating Skills", LogSeverity.InformationHigh);
            UpdateSkills(data);
            // TODO : Drop unmodded skill rows

            logger.Log(GetType(), "... Updating Skill lookup tables", LogSeverity.InformationHigh);
            UpdateSkillText(data);
            UpdateSkillCauses(data);
            UpdateSkillRemoves(data);
            UpdateRelatedSkills(data);
            UpdateSkillCategories(data);

            CompactImages();

            logger.Log(GetType(), "Writing data to disk", LogSeverity.InformationHigh);
            model.WriteXml(Data.GuildWars.Model.DataFile, XmlWriteMode.IgnoreSchema);

            logger.Log(GetType(), "Update complete", LogSeverity.InformationHigh);
        }

        #endregion

        private void PrepareExistingData()
        {
            model = new Data.GuildWars.Model();

            if(!File.Exists(Data.GuildWars.Model.DataFile))
                return;
            
            logger.Log(GetType(), "Loading previous data", LogSeverity.InformationHigh);
            model.ReadXml(Data.GuildWars.Model.DataFile);

            logger.Log(GetType(), "Preparing model", LogSeverity.InformationHigh);
            foreach(var row in model.Images)
            {
                if(row.ProfessionsRow != null)
                    row.ProfessionsRow.SetImageRefIdNull();

                var parentRows = row.GetParentRows("Skills_Images");
                if(parentRows != null && parentRows.Count() > 0)
                {
                    foreach(var parentRow in parentRows)
                        ((Data.GuildWars.Model.SkillsRow) parentRow).SetImageRefIdNull();
                }
            }

            model.Images.Clear();

            model.RelatedSkills.Clear();
            model.SkillsRemoves_Lookup.Clear();
            model.SkillsCauses_Lookup.Clear();
            model.SkillName.Clear();
            model.SkillDescription.Clear();
            model.SkillsCategories_Lookup.Clear();

            model.WriteXml("temp.xml", XmlWriteMode.IgnoreSchema);
            model = new Data.GuildWars.Model();

            model.ReadXml("temp.xml", XmlReadMode.Fragment);
            model.AcceptChanges();
        }

        private void UpdateSkillCategories(IEnumerable<Skill> enumerable)
        {
            foreach(var skill in enumerable)
            {
                if(skill.Categories != null)
                    foreach(var item in skill.Categories)
                    {
                        var row = model.SkillsCategories_Lookup.NewSkillsCategories_LookupRow();
                        row.SkillsId = CreateRowId(skill);
                        row.CategoriesId =
                            model.Categories.First(i => i.Name.Equals(item, StringComparison.InvariantCultureIgnoreCase))
                                .Id;
                        model.SkillsCategories_Lookup.AddSkillsCategories_LookupRow(row);
                    }
            }
        }

        private void UpdateRelatedSkills(IEnumerable<Skill> enumerable)
        {
            foreach(var skill in enumerable)
            {
                if(skill.Removes != null)
                    foreach(var item in skill.RelatedSkills)
                    {
                        var row = model.RelatedSkills.NewRelatedSkillsRow();
                        row.ParentSkillId = CreateRowId(skill);
                        row.RelatedSkillId = CreateRowId(item);
                        model.RelatedSkills.AddRelatedSkillsRow(row);
                    }
            }
        }

        private static int CreateRowId(int item)
        {
            return item + skillOffset;
        }

        private void UpdateSkillRemoves(IEnumerable<Skill> enumerable)
        {
            foreach(var skill in enumerable)
            {
                if(skill.Removes != null)
                    foreach(var item in skill.Removes)
                    {
                        var row = model.SkillsRemoves_Lookup.NewSkillsRemoves_LookupRow();
                        row.SkillsId = CreateRowId(skill);
                        row.RemovesId =
                            model.Removes.First(i => i.Name.Equals(item, StringComparison.InvariantCultureIgnoreCase)).
                                Id;
                        model.SkillsRemoves_Lookup.AddSkillsRemoves_LookupRow(row);
                    }
            }
        }

        private void UpdateSkillCauses(IEnumerable<Skill> enumerable)
        {
            foreach(var skill in enumerable)
            {
                if(skill.Causes != null)
                    foreach(var item in skill.Causes)
                    {
                        var row = model.SkillsCauses_Lookup.NewSkillsCauses_LookupRow();
                        row.SkillsId = CreateRowId(skill);
                        row.CausesId =
                            model.Causes.First(i => i.Name.Equals(item, StringComparison.InvariantCultureIgnoreCase)).
                                Id;
                        model.SkillsCauses_Lookup.AddSkillsCauses_LookupRow(row);
                    }
            }
        }

        private void UpdateSkillText(IEnumerable<Skill> enumerable)
        {
            foreach(var skill in enumerable)
            {
                if(skill.Name != null)
                    foreach(var name in skill.Name)
                    {
                        var row = model.SkillName.NewSkillNameRow();
                        row.Id = CreateRowId(skill);
                        row.Locale = name.Key;
                        row.Name = name.Value;

                        model.SkillName.AddSkillNameRow(row);
                    }

                if(skill.Description != null)
                    foreach(var text in skill.Description)
                    {
                        var row = model.SkillDescription.NewSkillDescriptionRow();
                        row.Id = CreateRowId(skill);
                        row.Locale = text.Key;
                        row.Description = text.Value;

                        if(skill.ConciseDescription != null && skill.ConciseDescription.ContainsKey(text.Key))
                            row.ConciseDescription = skill.ConciseDescription[text.Key];

                        model.SkillDescription.AddSkillDescriptionRow(row);
                    }
            }
        }

        private static int CreateRowId(Skill skill)
        {
            return skill.Id + skillOffset;
        }

        private void CompactImages()
        {
            foreach(var row in model.Images)
            {
                if(row.RowState == DataRowState.Unchanged)
                    row.Delete();
            }
        }

        private static string CreateDataString(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        private void UpdateSkills(IEnumerable<Skill> enumerable)
        {
            foreach(var skill in enumerable)
            {
                var skillRow = model.Skills.FindById(CreateRowId(skill));

                var newRow = (skillRow == null);

                if(newRow)
                {
                    skillRow = model.Skills.NewSkillsRow();
                    skillRow.Id = CreateRowId(skill);
                }
                skillRow.TemplateId = skill.Id;

                SetValue(skill,
                         skillRow,
                         i => i.ActivationTime,
                         ((r, v) => r.SourceActivationTime = v),
                         r => r.SetSourceActivationTimeNull());

                SetValue(skill,
                         skillRow,
                         i => i.Adrenaline,
                         ((r, v) => r.SourceAdrenalineCost = v),
                         r => r.SetSourceAdrenalineCostNull());

                SetValue(skill,
                         skillRow,
                         i => i.CausesExhaustion,
                         ((r, v) => r.SourceCausesExhaustion = v),
                         r => r.SetSourceCausesExhaustionNull());

                SetValue(skill,
                         skillRow,
                         i => i.EnergyCost,
                         ((r, v) => r.SourceEnergyCost = v),
                         r => r.SetSourceEnergyCostNull());

                SetValue(skill,
                         skillRow,
                         i => i.IsElite,
                         ((r, v) => r.SourceIsElite = v),
                         r => r.SetSourceIsEliteNull());

                SetValue(skill,
                         skillRow,
                         i => i.IsPvEOnly,
                         ((r, v) => r.SourceIsPvEOnly = v),
                         r => r.SetSourceIsPvEOnlyNull());

                SetValue(skill,
                         skillRow,
                         i => i.HasPvP,
                         ((r, v) => r.SourceHasPvP = v),
                         r => r.SetSourceHasPvPNull());

                SetValue(skill,
                         skillRow,
                         i => i.IsPvPVersion,
                         ((r, v) => r.SourceIsPvP = v),
                         r => r.SetSourceIsPvPNull());

                skillRow.IsRemoved = skill.IsRemoved;
                skillRow.IsValid = skill.IsValid;

                SetValue(skill,
                         skillRow,
                         i => i.RechargeTime,
                         ((r, v) => r.SourceRechargeTime = v),
                         r => r.SetSourceRechargeTimeNull());
                SetValue(skill,
                         skillRow,
                         i => i.Sacrifice,
                         ((r, v) => r.SourceSacrificeCost = v),
                         r => r.SetSourceSacrificeCostNull());

                SetValue(skill,
                         skillRow,
                         i => i.Upkeep,
                         ((r, v) => r.SourceUpkeepCost = v),
                         r => r.SetSourceUpkeepCostNull());

                skillRow.SourceWikiLink = skill.WikiLink;

                var lookupSkill = skill;

                skillRow.CampaignRefId =
                    model.Campaigns.First(
                        i => i.Name.Equals(lookupSkill.Campaign, StringComparison.InvariantCultureIgnoreCase)).Id;

                skillRow.ProfessionRefId =
                    model.Professions.First(
                        i => i.Name.Equals(lookupSkill.Profession, StringComparison.InvariantCultureIgnoreCase)).Id;

                skillRow.AreaOfEffectRefId =
                    model.AreaOfEffects.First(
                        i => i.Name.Equals(lookupSkill.AreaOfEffect, StringComparison.InvariantCultureIgnoreCase)).Id;

                skillRow.AttributeRefId =
                    model.Attributes.First(
                        i => i.Name.Equals(lookupSkill.Attribute, StringComparison.InvariantCultureIgnoreCase)).Id;

                skillRow.ProjectileRefId =
                    model.Projectiles.First(
                        i => i.Name.Equals(lookupSkill.Projectile, StringComparison.InvariantCultureIgnoreCase)).Id;

                skillRow.RangeRefId =
                    model.Ranges.First(
                        i => i.Name.Equals(lookupSkill.Range, StringComparison.InvariantCultureIgnoreCase)).Id;

                skillRow.SpecialTypeRefId =
                    model.SpecialTypes.First(
                        i => i.Name.Equals(lookupSkill.SpecialType, StringComparison.InvariantCultureIgnoreCase)).Id;

                skillRow.TargetRefId =
                    model.Targets.First(
                        i => i.Name.Equals(lookupSkill.Target, StringComparison.InvariantCultureIgnoreCase)).Id;

                skillRow.TypeRefId =
                    model.SkillTypes.First(
                        i => i.Name.Equals(lookupSkill.Type, StringComparison.InvariantCultureIgnoreCase)).Id;

                var imageRefId = UpdateSkillImage(skill.ImageId);

                if(!imageRefId.HasValue)
                    skillRow.SetImageRefIdNull();
                else
                    skillRow.ImageRefId = imageRefId.Value;

                if(newRow)
                    model.Skills.AddSkillsRow(skillRow);
            }
        }

        private int? UpdateSkillImage(string fileName)
        {
            if(!File.Exists(Path.Combine("images", fileName)))
                return null;

            var imageData = GetImage(fileName);

            var currentImage = model.Images.FirstOrDefault(i => CreateDataString(i.Data) == CreateDataString(imageData));

            if(currentImage == null)
            {
                currentImage = model.Images.NewImagesRow();
                currentImage.Data = imageData;
                model.Images.AddImagesRow(currentImage);
            }
            else
            {
                if(currentImage.RowState == DataRowState.Unchanged)
                    currentImage.SetModified();
            }

            return currentImage.Id;
        }

        private static void SetValue<T>(Skill source,
                                        Data.GuildWars.Model.SkillsRow target,
                                        Func<Skill, T?> valueSelector,
                                        Action<Data.GuildWars.Model.SkillsRow, T> targetSetter,
                                        Action<Data.GuildWars.Model.SkillsRow> nullSetter)
            where T : struct
        {
            var nullableValue = valueSelector(source);

            if(nullableValue.HasValue)
                targetSetter(target, nullableValue.Value);
            else
                nullSetter(target);
        }

        private void UpdateProfessionImages()
        {
            foreach(var profession in model.Professions)
            {
                var name = string.Format("{0}.png", Regex.Replace(profession.Name, "[^a-zA-Z ]", string.Empty));

                if(!File.Exists(Path.Combine("images", name)))
                    continue;

                var imageData = GetImage(name);


                var row = model.Images.AddImagesRow(imageData);
                profession.ImageRefId = row.Id;
            }
        }

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
            row.IsValidPrimary = true;

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

        private static byte[] GetImage(string imageName)
        {
            return ImageSerializer.GetImage(string.Format(@"images\{0}", imageName));
        }
    }
}