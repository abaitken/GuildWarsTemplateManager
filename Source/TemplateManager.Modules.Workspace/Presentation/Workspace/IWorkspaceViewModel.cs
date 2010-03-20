using System.Windows.Input;

namespace TemplateManager.Modules.Workspace.Presentation.Workspace
{
    public interface IWorkspaceViewModel
    {
        IWorkspaceView View { get; }
        ICommand AboutCommand { get; }
        ICommand HelpCommand { get; }
        ICommand CloseWindowCommand { get; }
        ICommand HelpTopicsCommand { get; }
        ICommand CloseTabCommand { get; }
        ICommand ShowUpdateCheckWindowCommand { get; }
        void OnViewLoaded();
    }
}