using System.Collections.Generic;
using TemplateManager.Infrastructure.Model;

namespace TemplateManager.Infrastructure.Services
{
    public interface IDataService
    {
        IEnumerable<IAttribute> Attributes { get; }
        IEnumerable<IProfession> SecondaryProfessions { get; }
        IEnumerable<ISkill> Skills { get; }
        IProfession EmptyProfession { get; }
        IEnumerable<IProfession> PrimaryProfessions { get; }
        IEnumerable<IProfession> Professions { get; }
    }
}