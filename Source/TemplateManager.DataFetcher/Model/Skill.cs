using System.Collections.Generic;

namespace TemplateManager.DataFetcher.Model
{
    public class Skill
    {
        public int Id { get; set; }
        public string WikiLink { get; set; }

        public Dictionary<string, string> Name { get; set; }
        public Dictionary<string, string> Description { get; set; }
        public Dictionary<string, string> ConciseDescription { get; set; }

        public double? ActivationTime { get; set; }
        public double? RechargeTime { get; set; }
        public double? EnergyCost { get; set; }
        public double? Sacrifice { get; set; }
        public double? Adrenaline { get; set; }
        public double? Upkeep { get; set; }
        public bool? CausesExhaustion { get; set; }


        public string Campaign { get; set; }
        public string Profession { get; set; }
        public string Attribute { get; set; }

        public string Type { get; set; }
        public string SpecialType { get; set; }
        public string Range { get; set; }
        public string Target { get; set; }
        public string Projectile { get; set; }

        public List<string> Removes { get; set; }
        public List<int> RelatedSkills { get; set; }
        public List<string> Causes { get; set; }
        public List<string> Categories { get; set; }

        public bool IsValid { get; set; }
        public bool? IsElite { get; set; }
        public bool? IsPvEOnly { get; set; }
        public bool? IsPvPVersion { get; set; }
        public bool? HasPvP { get; set; }

        public string ImageId { get; set; }

        public bool IsRemoved { get; set; }

        public string AreaOfEffect { get; set; }
    }
}