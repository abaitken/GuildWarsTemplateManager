using TemplateManager.DataFetcher.Model;

namespace TemplateManager.DataFetcher.Parsers
{
    public interface IParser
    {
        RawSkillData Fetch(int skillId);
        RawSkillData Fetch(int skillId, string wikiPageArticle);
    }
}