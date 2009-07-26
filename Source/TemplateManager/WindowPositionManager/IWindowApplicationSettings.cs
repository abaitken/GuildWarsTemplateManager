using System.Windows;

namespace TemplateManager.WindowPositionManager
{
    public interface IWindowApplicationSettings
    {
        Rect Location { get; set; }
        WindowState WindowState { get; set; }
        void Reload();
        void Save();
    }
}