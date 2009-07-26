using System.Collections.Generic;
using TemplateManager.Infrastructure.Model;

namespace TemplateManager.Infrastructure.Services
{
    public interface IDataService
    {
        IEnumerable<SkillAttribute> Attributes { get; }
        IEnumerable<Profession> SecondaryProfessions { get; }
        IEnumerable<Skill> Skills { get; }
        Profession EmptyProfession { get; }
        IEnumerable<Profession> PrimaryProfessions { get; }
        IEnumerable<Profession> Professions { get; }
    }
}