using System.Windows.Input;

namespace TemplateManager.UpdateCheck
{
    public interface IUpdateCheckViewModel
    {
        IUpdateCheckView View { get; }
        string CurrentVersion { get; }
        string LatestVersion { get; }
        string InformationUrl { get; }
        ICommand CloseWindowCommand { get; }
    }
}