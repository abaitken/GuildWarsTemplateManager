using System.Windows.Media.Imaging;

namespace TemplateManager.Assets
{
    public class NativeSkill
    {
        public int Id { get; set; }
        public int SkillId { get; set; }
        public int Campaign { get; set; }
        public int Profession { get; set; }
        public int Attribute { get; set; }
        public int SkillType { get; set; }
        public float EnergyCost { get; set; }
        public float CastingTime { get; set; }
        public float RechargeTime { get; set; }
        public float AdrenalineCost { get; set; }
        public float HealthSacrifice { get; set; }
        public float EnergyRegen { get; set; }
        public bool IsElite { get; set; }
        public bool PveOnly { get; set; }
        public bool PvpVersion { get; set; }
        public bool MonsterOnly { get; set; }
        public string Name { get; set; }
        public BitmapImage Image { get; set; }
        public string Tags { get; set; }
        public string WikiLink { get; set; }
        public string Description { get; set; }
    }
}