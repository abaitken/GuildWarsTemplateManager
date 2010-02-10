using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using TemplateManager.DataFetcher.Abstractions;
using TemplateManager.DataFetcher.DataProviders;
using TemplateManager.DataFetcher.Logging;
using TemplateManager.DataFetcher.Model;
using System.Diagnostics;

namespace TemplateManager.DataFetcher.Parsers
{
    internal class Parser : IParser
    {
        private readonly IDataProvider dataProvider;
        private readonly IFileSystemAbstraction fileSystem;
        private readonly string imageStore;
        private readonly ILogger logger;

        public Parser(ILogger logger,
                      IDataProvider dataProvider,
                      IFileSystemAbstraction fileSystem,
                      string imageStore)
        {
            this.logger = logger;
            this.dataProvider = dataProvider;
            this.fileSystem = fileSystem;
            this.imageStore = imageStore;
        }

        #region IParser Members

        public RawSkillData Fetch(int skillId, string wikiPageArticle)
        {
            var basicName = CreateBasicName(wikiPageArticle);
            var rawWikiPage = dataProvider.RequestData(basicName, true);


            var result = new RawSkillData
                             {
                                 WikiLink = dataProvider.CreateUrl(basicName, false),
                                 SkillId = skillId,
                                 BasicName = basicName
                             };

            if(string.IsNullOrEmpty(rawWikiPage))
            {
                logger.Log(GetType(),
                           string.Format("Wiki article not found. skillId: {0}, basicName: {1}",
                                         skillId,
                                         basicName),
                           LogSeverity.Warning);

                result.NotASkill = true;
                return result;
            }

            result.IsRemoved = HasBeenRemoved(rawWikiPage);

            var pageData = ReplaceDifficultTemplate(rawWikiPage);

            var skillBox = GetSkillBoxContent(pageData);

            if(string.IsNullOrEmpty(skillBox))
            {
                var severity = Regex.Match(pageData, "skill infobox").Success ? LogSeverity.Error : LogSeverity.Warning;
                logger.Log(GetType(),
                           string.Format("No skill info box found. skillId: {0}, basicName: {1}",
                                         skillId,
                                         basicName),
                           severity);
                result.NotASkill = true;
                return result;
            }


            var values = ExtractSkillBoxValues(skillBox);


            var id = values.GetIntegerValue("id");

            if(id.HasValue)
            {
                if(id.Value != skillId)
                    logger.Log(GetType(),
                               string.Format(
                                   "Request skill id does no match skill from dataProvider. skillId: {0}, basicName: {1}, wikiId: {2}",
                                   skillId,
                                   basicName,
                                   values["id"]),
                               LogSeverity.Warning);

                result.SecondarySkillId = id;
            }

            result.Name = values.GetTextValue("name");

            var description = values.GetTextValue("description");
            var conciseDescription = values.GetTextValue("concise description");
            if(string.IsNullOrEmpty(description))
                description = conciseDescription;

            result.Description = description;
            result.ConciseDescription = conciseDescription;
            result.Type = values.GetTextValue("type");
            result.Campaign = values.GetTextValue("campaign");
            result.Profession = values.GetTextValue("profession");
            result.Attribute = values.GetTextValue("attribute");
            result.IsElite = values.GetBoolValue("elite");
            result.EnergyCost = values.GetDoubleValue("energy");
            result.ActivationTime = values.GetActivationTime();
            result.RechargeTime = values.GetDoubleValue("recharge");
            result.Upkeep = values.GetDoubleValue("upkeep");
            result.Adrenaline = values.GetDoubleValue("adrenaline");
            result.Sacrifice = values.GetDoubleValue("sacrifice");
            result.Exhaustion = values.GetBoolValue("exhaustion");
            result.Target = values.GetTextValue("target");
            result.Range = values.GetTextValue("range");
            result.SpecialType = GetSpecialType(values, rawWikiPage);
            result.IsPvEOnly = values.GetBoolValue("pve-only");
            result.HasPvP = values.GetBoolValue("has-pvp");
            result.PvEVersion = GetPvEVersion(rawWikiPage);
            result.IsPvPVersion = values.GetBoolValue("is-pvp");
            result.Projectile = values.GetTextValue("projectile");
            result.AreaOfEffect = values.GetTextValue("aoe");

            result.Causes = values.GetInfoBoxValueArray(new[]
                                                            {
                                                                "causes1", "causes2", "causes3"
                                                            });
            result.Removes = values.GetInfoBoxValueArray(new[]
                                                             {
                                                                 "removes1", "removes2"
                                                             });

            result.RelatedSkills = ExtractRelatedSkills(pageData);

            result.Progression = GetProgression(pageData);
            result.Categories = GetCategories(pageData);


            var path = Path.Combine(imageStore, string.Format("{0}.jpg", skillId));

            if(!fileSystem.Exists(path))
            {
                var image = GetImage(basicName);

                if(image == null)
                {
                    logger.Log(GetType(),
                               string.Format("Skill image not found. skillId: {0}, basicName: {1}",
                                             skillId,
                                             basicName),
                               LogSeverity.Warning);
                }
                else
                {
                    fileSystem.Write(path, image);
                }
            }

            return result;
        }

        public RawSkillData Fetch(int skillId)
        {
            logger.Log(GetType(), string.Format("Processing skill. skillId: {0}", skillId), LogSeverity.InformationHigh);

            var wikiPageArticle = GetWikiArticle(skillId);

            if(wikiPageArticle == null)
            {
                logger.Log(GetType(),
                           string.Format("Article not found for skill. skillId: {0}", skillId),
                           LogSeverity.Warning);

                return new RawSkillData
                           {
                               SkillId = skillId,
                               NotASkill = true
                           };
            }

            return Fetch(skillId, wikiPageArticle);
        }

        #endregion

        private static List<string> GetCategories(string data)
        {
            var resultQuery =
                from Match match in
                    Regex.Matches(data, @"\[\[.*?category:\s*(?<Category>[^\|\]]*)", RegexOptions.IgnoreCase)
                select match.Groups["Category"].Value.Trim();

            var result = resultQuery.ToList();
            return result;
        }

        private Progression GetProgression(string data)
        {
            // TODO : Lookup max level
            var match = Regex.Match(data,
                                    @"\{\{\s*skill progression\s*(?<MaxLevel>.*?)\s*\|\s*(?:attribute|title track)\s*=\s*(?<Attribute>.*?)\s*(?<Vars>\|.*?)\s*\}\}",
                                    RegexOptions.Singleline | RegexOptions.IgnoreCase);

            if(!match.Success)
            {
                if(data.ToLower().Contains("skill progression"))
                    logger.Log(GetType(),
                               "Failed to find skill progression when skill progression block exists",
                               LogSeverity.Error);
                return null;
            }

            var maxLevel = match.Groups["MaxLevel"].Value;
            maxLevel = maxLevel.Replace("max", string.Empty);

            var result = new Progression
                             {
                                 Attribute = Regex.Replace(match.Groups["Attribute"].Value, @"\[|\]", string.Empty),
                                 MaxLevel = maxLevel
                             };
            maxLevel = "at" + (string.IsNullOrEmpty(maxLevel) ? "15" : maxLevel);

            var varsSection = match.Groups["Vars"].Value;

            var vars = new Dictionary<string, ProgressionVar>();

            var matches = Regex.Matches(varsSection,
                                        @"^\|\s*(?<Level>var\d)\s*(?<Item>.*?)\s*=\s*(?<Value>.*?)\s*$",
                                        RegexOptions.Multiline | RegexOptions.IgnoreCase);

            foreach(Match varMatch in matches)
            {
                var level = varMatch.Groups["Level"].Value.ToLower();

                if(!vars.ContainsKey(level))
                    vars.Add(level, new ProgressionVar());

                var item = varMatch.Groups["Item"].Value.ToLower();
                var value = varMatch.Groups["Value"].Value;

                if(item == "name")
                {
                    vars[level].Name = Regex.Replace(value, @"\[|\]", string.Empty);
                    continue;
                }

                if(item == "at0")
                {
                    vars[level].Level0 = value;
                    continue;
                }

                if(item == maxLevel)
                {
                    vars[level].MaxLevel = value;
                    continue;
                }
                            
                logger.Log(GetType(), string.Format("Unknown progression var: {0}", item), LogSeverity.Error);
            }

            result.Vars = vars.Values;

            return result;
        }

        private static string GetSpecialType(IDictionary<string, string> values, string page)
        {
            var result = values.GetTextValue("special");

            if(result != null)
                return result;

            return Regex.IsMatch(page, @"\[\[Category:Dev content\]\]", RegexOptions.IgnoreCase) ? "Dev Content" : null;
        }

        private static string GetPvEVersion(string page)
        {
            var match = Regex.Match(page, @"\{\{pveversion\|\s*(?<Skill>.*?)\s*\}\}", RegexOptions.IgnoreCase);

            return match.Success ? match.Groups["Skill"].Value : null;
        }

        private Bitmap GetImage(string article)
        {
            var imageAddress = GetImageAddress(article);

            if(imageAddress == null)
                return null;

            return dataProvider.RequestImage(imageAddress);
        }

        private string GetImageAddress(string article)
        {
            var htmlPage = dataProvider.RequestData(article, false);

            if(htmlPage == null)
                return null;


            var match = Regex.Match(htmlPage,
                                    @"<div class=""skill-image"">.*?src=""(?<Image>.*?)""",
                                    RegexOptions.Singleline);

            return match.Success ? match.Groups["Image"].Value : null;
        }

        private static List<string> ExtractRelatedSkills(string data)
        {
            var result = new List<string>();
            var relatedSkills = Regex.Match(data,
                                            @"==Related skills==\s*(?<Section>.*?)(?:==|$)",
                                            RegexOptions.Singleline);
            if(!relatedSkills.Success)
                return result;


            var matches = Regex.Matches(relatedSkills.Value,
                                        @"^\s*\*\s*\[Template\]skill icon\|(?<RelatedSkill>.*?)\[/Template\]",
                                        RegexOptions.Multiline);

            if(matches.Count == 0)
                return result;

            foreach(Match match in matches)
                result.Add(match.Groups["RelatedSkill"].Value);

            return result;
        }

        private IDictionary<string, string> ExtractSkillBoxValues(string skillBox)
        {
            var temp = ParseSkillBox(skillBox);

            var result = new Dictionary<string, string>();

            var values = Regex.Split(temp, @"\|", RegexOptions.Multiline);

            foreach(var value in values)
            {
                if(value.Trim() == string.Empty)
                    continue;

                var match = Regex.Match(value, @"\s*(?<Key>.*?)\s*=\s*(?<Value>.*)\s*", RegexOptions.Singleline);

                if(!match.Success)
                {
                    logger.Log(GetType(),
                               string.Format("Failed to resolve info box values: {0}", value),
                               LogSeverity.Warning);
                    continue;
                }

                var key = match.Groups["Key"].Value.ToLower();

                try
                {
                    result.Add(key, match.Groups["Value"].Value.Trim());
                }
                catch(ArgumentException)
                {
                    logger.Log(GetType(), string.Format("InfoBox already contains value: {0}", key), LogSeverity.Warning);
                }
            }

            return result;
        }

        private static string ParseSkillBox(string box)
        {
            var result = Regex.Replace(box,
                                       @"<span style=""color:(.*?);"">(.*?)</span>",
                                       "[color=$1]$2[/color]",
                                       RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Mostly marked p grey text: <span style="color:gray;">
            // Regex replace all: <(.|\n)*? > or <(.*?|\n*?)>

            // Reverse out template matching
            // And replace other undesired elements
            result = Regex.Replace(result, @"\[Template\]", @"{{");
            result = Regex.Replace(result, @"\[/Template\]", @"}}");
            result = Regex.Replace(result, @"\{\{sic\}\}", string.Empty, RegexOptions.IgnoreCase);
            result = Regex.Replace(result,
                                   @"\{\{grey\|(.*)\}\}",
                                   @"$1",
                                   RegexOptions.IgnoreCase);

            result = GreenTemplateParser.Parse(result);

            // Replace all existing templates in the text

            // Repalce links with link text: [[Article]] with Article
            result = Regex.Replace(result,
                                   @"\[\[(.[^\|]*?)\]\]",
                                   @"$1",
                                   RegexOptions.Singleline | RegexOptions.Multiline);

            // Replace links with text: [[Article|Text]] with Text
            result = Regex.Replace(result,
                                   @"\[\[.*?\|(.*?)\]\]",
                                   @"$1",
                                   RegexOptions.Singleline | RegexOptions.Multiline);

            return result;
        }

        private static bool HasBeenRemoved(string page)
        {
            return Regex.IsMatch(page, @"\{\{Historical content\}\}", RegexOptions.IgnoreCase);
        }

        private static string GetSkillBoxContent(string data)
        {
            var match = Regex.Match(data,
                                    @"\{\{skill infobox\s*(?<Info>.*?)\}\}",
                                    RegexOptions.Singleline | RegexOptions.IgnoreCase);

            return match.Success ? match.Groups["Info"].Value : string.Empty;
        }

        private static string ReplaceDifficultTemplate(string page)
        {
            return Regex.Replace(page, @"\{\{(.*?)\}\}", @"[Template]$1[/Template]");
        }

        private string GetWikiArticle(int id)
        {
            var gameLinkArticle = string.Format("Game_link:Skill_{0}", id);
            var gameLinkData = dataProvider.RequestData(gameLinkArticle, true);

            if(gameLinkData == null)
                return null;

            var match = Regex.Match(gameLinkData, @"#REDIRECT\s*\[\[self:(?<WikiPage>.*?)\]\]", RegexOptions.IgnoreCase);

            if(!match.Success)
                return null;

            var result = match.Groups["WikiPage"].Value;

            return result;
        }

        private static string CreateBasicName(string result)
        {
            return HttpUtility.UrlDecode(result).Trim().Replace(' ', '_');
        }
    }
}