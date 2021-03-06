using System.Windows;

namespace TemplateManager.Common
{
    public interface IWindowSettings
    {
        Rect Location { get; set; }
        WindowState WindowState { get; set; }
        void Reload();
        void Save();
    }
}