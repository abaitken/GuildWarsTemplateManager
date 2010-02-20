using System;
using System.Windows.Media.Imaging;

namespace TemplateManager.Infrastructure.Model
{
    public interface IProfession : IEquatable<IProfession>
    {
        int TemplateId { get; }
        string Name { get; }
        bool IsValid { get; }
        bool IsValidPrimary { get; }
        BitmapImage Image { get; }
    }
}