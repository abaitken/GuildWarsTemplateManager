using TemplateManager.Common.CommandModel;

namespace TemplateManager.MainView
{
    public interface IMainViewModel
    {
        IMainView View { get; }        
        ICommandModel ShowOptionsCommand { get; }
        ICommandModel AboutCommand { get; }
        ICommandModel HelpCommand { get; }
        ICommandModel CloseWindowCommand { get; }
        ICommandModel HelpTopicsCommand { get; }
        ICommandModel TemplatesViewCommand { get; }
        ICommandModel DuplicateTemplatesViewCommand { get; }
        ICommandModel CloseTabCommand { get; }
        ICommandModel ShowUpdateCheckWindowCommand { get; }
        void OnViewLoaded();
    }
}