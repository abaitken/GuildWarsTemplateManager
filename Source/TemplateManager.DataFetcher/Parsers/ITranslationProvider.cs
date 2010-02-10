using System.Collections.Generic;

namespace TemplateManager.DataFetcher.Parsers
{
    internal interface ITranslationProvider
    {
        bool IndexBuilt { get; }
        Dictionary<string, string> FetchSkillName(string skillName, string englishString);
        void Build();
        Dictionary<string, string> FetchDescription(string skillName, string englishString);
        Dictionary<string, string> FetchConciseDescription(string skillName, string englishString);
    }
}