using System.Windows.Media.Imaging;

namespace TemplateManager.Infrastructure.Model
{
    public interface IProfession
    {
        int TemplateId { get; }
        string Name { get; }
        bool IsValid { get; }
        bool IsValidPrimary { get; }
        BitmapImage Image { get; }
    }
}