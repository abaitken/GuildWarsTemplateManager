using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace TemplateManager.Infrastructure.Model
{
    public interface ISkill
    {
        int TemplateId { get; }
        string WikiLink { get; }

        string Name { get; }
        string Description { get; }
        string ConciseDescription { get; }

        double? ActivationTime { get; }
        double? RechargeTime { get; }
        double? EnergyCost { get; }
        double? Sacrifice { get; }
        double? AdrenalineCost { get; }
        double? UpkeepCost { get; }
        bool? CausesExhaustion { get; }

        string Campaign { get; }
        IProfession Profession { get; }
        IAttribute Attribute { get; }

        string Type { get; }
        string SpecialType { get; }
        string Range { get; }
        string Target { get; }
        string Projectile { get; }
        string AreaOfEffect { get; }

        IEnumerable<string> Removes { get; }
        IEnumerable<ISkill> RelatedSkills { get; }
        IEnumerable<string> Causes { get; }
        IEnumerable<string> Categories { get; }

        bool? IsElite { get; }
        bool? IsPvEOnly { get; }
        bool? IsPvPVersion { get; }
        bool? HasPvP { get; }

        BitmapImage Image { get; }

        bool IsRemoved { get; }
        bool IsValid { get; }
    }
}
