using System.Windows;

namespace TemplateManager.AboutView
{
    public interface IAboutView
    {
        IAboutViewModel Model { get; set; }
    }
}