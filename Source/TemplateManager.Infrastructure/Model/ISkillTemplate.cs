using System.Collections.Generic;

namespace TemplateManager.Infrastructure.Model
{
    public interface ISkillTemplate
    {
        string TemplateCode { get; }
        string BuildFile { get; }
        string Name { get; }
        IProfession PrimaryProfession { get; }
        IProfession SecondaryProfession { get; }
        IList<ISkill> Skills { get; }
        IEnumerable<AttributeValue> Attributes { get; }
        bool IsValid { get; }
        string Author { get; }
        string Tags { get; }
        string Notes { get; }
        int SkillKey { get; }
    }
}