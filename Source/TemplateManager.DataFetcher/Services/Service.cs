using System;
using System.Collections.Generic;
using System.Linq;
using TemplateManager.DataFetcher.DataTargets;
using TemplateManager.DataFetcher.Logging;
using TemplateManager.DataFetcher.Model;
using TemplateManager.DataFetcher.Parsers;

namespace TemplateManager.DataFetcher.Services
{
    internal class Service : IService
    {
        private readonly int end;
        private readonly ILogger logger;
        private readonly IParser parser;
        private readonly int start;
        private readonly IEnumerable<IDataTarget> targets;
        private readonly ITranslationProvider translationProvider;

        public Service(ILogger logger,
                       IParser parser,
                       IEnumerable<IDataTarget> targets,
                       ITranslationProvider translationProvider,
                       int start,
                       int end)
        {
            this.logger = logger;
            this.parser = parser;
            this.targets = targets;
            this.translationProvider = translationProvider;
            this.start = start;
            this.end = end;
        }

        #region IService Members

        public void Execute()
        {
            translationProvider.Build();
            var skills = GetSkills();

            logger.Log(GetType(), "Updating targets", LogSeverity.InformationHigh);
            foreach(var target in targets)
                target.Update(skills);

            logger.Log(GetType(), "Service complete", LogSeverity.InformationHigh);
        }

        #endregion

        private IEnumerable<Skill> GetSkills()
        {
            var skillData = GetRawSkillData();
            var skillIndex = BuildIndex(skillData);

            // TODO : Pair up skills to make it easier to switch between PvE and PvP

            return BuildWellFormedData(skillData, skillIndex).ToList();
        }

        private IDictionary<string, RawSkillData> BuildIndex(IEnumerable<RawSkillData> skills)
        {
            var result = new Dictionary<string, RawSkillData>();

            foreach(var rawSkill in skills)
            {
                if(string.IsNullOrEmpty(rawSkill.BasicName))
                    continue;

                var key = rawSkill.BasicName.ToLower();

                if(result.ContainsKey(key))
                {
                    logger.Log(GetType(),
                               string.Format("Duplicate skill detected. skillId: {0}, baseName: {1}",
                                             rawSkill.SkillId,
                                             key),
                               LogSeverity.Warning);
                    continue;
                }

                result.Add(key, rawSkill);
            }

            return result;
        }

        private const string none = "<None>";

        private IEnumerable<Skill> BuildWellFormedData(IEnumerable<RawSkillData> skills,
                                                       IDictionary<string, RawSkillData> index)
        {
            foreach(var data in skills)
            {
                if(data.NotASkill)
                {
                    yield return new Skill
                                     {
                                         Id = data.SkillId,
                                         IsValid = false,
                                         ImageId = "unknown.jpg",
                                     };
                    continue;
                }

                var relatedSkills = from skillName in data.RelatedSkills
                                    let key = skillName.ToLower()
                                    where index.ContainsKey(key)
                                    select index[key].SkillId;

                // TODO : Add skill progression information
                yield return new Skill
                                 {
                                     Id = data.SkillId,
                                     WikiLink = data.WikiLink,
                                     Name = translationProvider.FetchSkillName(data.BasicName, data.Name),
                                     Description =
                                         translationProvider.FetchDescription(data.BasicName, data.Description),
                                     ConciseDescription =
                                         translationProvider.FetchConciseDescription(data.BasicName,
                                                                                     data.ConciseDescription),
                                     Campaign =  GetValue(data.Campaign),
                                     Profession = GetValue(data.Profession),
                                     Attribute = GetValue(data.Attribute),
                                     Type = GetValue(data.Type),
                                     Range = GetValue(data.Range),
                                     Target = GetValue(data.Target),
                                     AreaOfEffect = GetValue(data.AreaOfEffect),
                                     Projectile = GetValue(data.Projectile),
                                     SpecialType = GetValue(data.SpecialType),

                                     ActivationTime = data.ActivationTime,
                                     RechargeTime = data.RechargeTime,
                                     EnergyCost = data.EnergyCost,
                                     Sacrifice = data.Sacrifice,
                                     Adrenaline = data.Adrenaline,
                                     Upkeep = data.Upkeep,

                                     IsElite = data.IsElite,
                                     IsRemoved = data.IsRemoved,
                                     IsPvEOnly = data.IsPvEOnly,
                                     IsPvPVersion = data.IsPvPVersion,
                                     CausesExhaustion = data.Exhaustion,
                                     HasPvP = data.HasPvP,
                                     IsValid = !data.IsRemoved || string.IsNullOrEmpty(data.SpecialType),

                                     RelatedSkills = relatedSkills.ToList(),
                                     Causes = data.Causes,
                                     Removes = data.Removes,
                                     Categories = data.Categories,

                                     ImageId = string.Format("{0}.jpg", data.SkillId),
                                     //Progression = data.Progression
                                 };
            }
        }

        private string GetValue(string value)
        {
            return string.IsNullOrEmpty(value) ? none : value;
        }


        private IEnumerable<RawSkillData> GetRawSkillData()
        {
            var linkedSkillsQuery = from id in Enumerable.Range(start, end)
                                    let skill = parser.Fetch(id)
                                    where skill != null
                                    select skill;

            var linkedSkills = linkedSkillsQuery.ToList();

            // TODO : Consider not having all of the data

            return linkedSkills;
        }
    }
}