using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TemplateManager.DataFetcher.DataProviders;
using TemplateManager.DataFetcher.Logging;

namespace TemplateManager.DataFetcher.Parsers
{
    internal class TranslationProvider : ITranslationProvider
    {
        private readonly IDictionary<string, string> columnMap = new Dictionary<string, string>
                                                                     {
                                                                         {
                                                                             "english",
                                                                             "en"
                                                                             },
                                                                         {
                                                                             "german",
                                                                             "de"
                                                                             },
                                                                         {
                                                                             "french",
                                                                             "fr"
                                                                             },
                                                                         {
                                                                             "korean",
                                                                             "ko"
                                                                             },
                                                                         {
                                                                             "italian",
                                                                             "it"
                                                                             },
                                                                         {
                                                                             "spanish",
                                                                             "es"
                                                                             },
                                                                         {
                                                                             "polish",
                                                                             "pl"
                                                                             },
                                                                         {
                                                                             "japanese",
                                                                             "ja"
                                                                             },
                                                                         {
                                                                             "chinese (trad.)",
                                                                             "zh-TW"
                                                                             },
                                                                         {
                                                                             "chinese (simp.)",
                                                                             "zh-CN"
                                                                             }
                                                                     };

        private readonly IDataProvider dataProvider;
        private readonly ILogger logger;

        private readonly IEnumerable<string> pages = new[]
                                                         {
                                                             "Localized warrior skill names",
                                                             "Localized ranger skill names",
                                                             "Localized monk skill names",
                                                             "Localized necromancer skill names",
                                                             "Localized mesmer skill names",
                                                             "Localized elementalist skill names",
                                                             "Localized assassin skill names",
                                                             "Localized ritualist skill names",
                                                             "Localized paragon skill names",
                                                             "Localized dervish skill names"
                                                         };

        private IDictionary<string, IDictionary<string, string>> translations;

        public TranslationProvider(IDataProvider dataProvider, ILogger logger)
        {
            this.dataProvider = dataProvider;
            this.logger = logger;
        }

        #region ITranslationProvider Members

        public Dictionary<string, string> FetchSkillName(string skillName, string englishString)
        {
            if(!IndexBuilt)
                throw new InvalidOperationException(
                    "Build must be called before operations to fetch translations can be performed");

            var key = skillName.ToLower();
            var translated = from cultureStrings in translations
                             where cultureStrings.Value.ContainsKey(key)
                             let culture = cultureStrings.Key
                             let localizedString = cultureStrings.Value[key]
                             select new
                                        {
                                            culture,
                                            localizedString
                                        };

            var result = translated.ToDictionary(k => k.culture, v => v.localizedString);
            result.Add("en", englishString);

            return result;
        }

        public void Build()
        {
            if(IndexBuilt)
                throw new InvalidOperationException("Build cannot be called more than once");

            translations = new Dictionary<string, IDictionary<string, string>>();

            var pageData = GetPages();

            var headers = (from Match match in Regex.Matches(pageData, "^!(?<Header>.*?)$", RegexOptions.Multiline)
                           select match.Value).Distinct();

            if(headers.Count() != 1)
            {
                logger.Log(GetType(), "Page headers are different", LogSeverity.Error);
                return;
            }

            var header = headers.First();

            var matches = Regex.Split(header, @"^!|!!|!$", RegexOptions.Multiline);

            if(matches.Length == 0)
            {
                logger.Log(GetType(),
                           "Failed to locate columns in header",
                           LogSeverity.Warning);
                return;
            }

            var columns = new List<string>();

            foreach(var match in matches)
            {
                var columnName = match.Trim();

                if(columnName == string.Empty)
                    continue;

                var culture = columnMap[columnName.ToLower()];
                columns.Add(culture);
            }

            var rowQuery = from Match row in Regex.Matches(pageData, @"^\s*\|\s(?<Row>.*?)$", RegexOptions.Multiline)
                           let cells = from cell in Regex.Split(row.Value, @"^\||\|\|", RegexOptions.Multiline)
                                       let s = cell.Trim()
                                       where s != string.Empty
                                       select s.Trim('[', ']')
                           select cells.ToList();


            var rows = rowQuery.ToList();

            Debug.Assert(columns.Contains("en"));
            var englishIndex = columns.IndexOf("en");

            // Iterate over all of the rows
            foreach(var row in rows)
            {
                // Iterate over all of columns
                for(var i = 0; i < columns.Count; i++)
                {
                    // If the current column is the english column then skip
                    if(i == englishIndex)
                        continue;

                    // Get the current culture
                    var culture = columns[i];

                    // If the translation look up dictinary does not contain an index for the current culture then create one
                    if(!translations.ContainsKey(culture))
                        translations.Add(culture, new Dictionary<string, string>());

                    var key = row[englishIndex].ToLower();
                    if(translations[culture].ContainsKey(key))
                    {
                        logger.Log(GetType(),
                                   string.Format("String already added for skill. skill: {0}, culture: {1}",
                                                 row[englishIndex],
                                                 culture),
                                   LogSeverity.Warning);
                        break;
                    }
                    
                    // Associate this culture, with the skill name and its translated string
                    translations[culture].Add(key, row[i]);
                }
            }

            return;
        }

        public bool IndexBuilt
        {
            get { return translations != null; }
        }

        public Dictionary<string, string> FetchDescription(string skillName, string englishString)
        {
            return CreateDefaultLanguageCollection(englishString);
        }

        public Dictionary<string, string> FetchConciseDescription(string skillName, string englishString)
        {
            return CreateDefaultLanguageCollection(englishString);
        }

        #endregion

        private static Dictionary<string, string> CreateDefaultLanguageCollection(string englishString)
        {
            var result = new Dictionary<string, string>();

            if(!string.IsNullOrEmpty(englishString))
                result.Add("en", englishString);

            return result;
        }

        private string GetPages()
        {
            var pageBuilder = new StringBuilder();

            foreach(var page in pages)
            {
                var pageData = dataProvider.RequestData(page, true);

                if(pageData == null)
                {
                    logger.Log(GetType(), string.Format("Failed to request page. page: {0}", page), LogSeverity.Warning);
                    continue;
                }

                pageBuilder.AppendLine(pageData);
            }

            return pageBuilder.ToString();
        }
    }
}