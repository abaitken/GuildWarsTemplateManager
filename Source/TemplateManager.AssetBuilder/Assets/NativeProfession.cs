using System.Windows.Media.Imaging;

namespace TemplateManager.Assets
{
    public class NativeProfession
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int ProfessionId { get; set; }
        public bool ValidPrimary { get; set; }
        public bool ValidSecondary { get; set; }
        public BitmapImage Image { get; set; }
    }
}