using System;
using System.Collections.Generic;

namespace TemplateManager.DataFetcher.Model
{
    public class RawSkillData
    {
        public Progression Progression { get; set; }
        public List<string> Causes { get; set; }
        public int SkillId { get; set; }
        public string WikiLink { get; set; }
        public string BasicName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ConciseDescription { get; set; }
        public string Campaign { get; set; }
        public string Profession { get; set; }
        public string Attribute { get; set; }
        public string Type { get; set; }
        public double? ActivationTime { get; set; }
        public double? RechargeTime { get; set; }
        public double? EnergyCost { get; set; }
        public bool? IsElite { get; set; }
        public bool IsRemoved { get; set; }
        public string SpecialType { get; set; }
        public bool? IsPvEOnly { get; set; }
        public bool? IsPvPVersion { get; set; }
        public List<string> RelatedSkills { get; set; }
        public List<string> Removes { get; set; }
        public string AreaOfEffect { get; set; }
        public string Projectile { get; set; }
        public bool? HasPvP { get; set; }
        public string Range { get; set; }
        public string Target { get; set; }
        public bool? Exhaustion { get; set; }
        public double? Sacrifice { get; set; }
        public double? Adrenaline { get; set; }
        public double? Upkeep { get; set; }
        public string PvEVersion { get; set; }
        public bool NotASkill { get; set; }
        public int? SecondarySkillId { get; set; }

        public List<string> Categories { get; set; }


        public override string ToString()
        {
            return string.Format("SkillId: {0}, BasicName: {1}", SkillId, BasicName);
        }
    }
}