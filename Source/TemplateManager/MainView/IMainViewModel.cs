using System.Windows.Input;
using TemplateManager.Common.CommandModel;

namespace TemplateManager.MainView
{
    public interface IMainViewModel
    {
        IMainView View { get; }        
        ICommand ShowOptionsCommand { get; }
        ICommand AboutCommand { get; }
        ICommand HelpCommand { get; }
        ICommand CloseWindowCommand { get; }
        ICommand HelpTopicsCommand { get; }
        ICommandModel TemplatesViewCommand { get; }
        ICommandModel DuplicateTemplatesViewCommand { get; }
        ICommandModel CloseTabCommand { get; }
        ICommand ShowUpdateCheckWindowCommand { get; }
        void OnViewLoaded();
    }
}