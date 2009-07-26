using System.Windows;

namespace TemplateManager.AboutView
{
    public interface IAboutView
    {
        IAboutViewModel Model { get; set; }
        Window Owner { get; set; }
        bool? ShowDialog();
    }
}