using TemplateManager.Common.CommandModel;

namespace TemplateManager.ShellView
{
    public interface IShellViewModel
    {
        IShellView View { get; }
        string WindowTitle { get; }
        void ShowView();
        ICommandModel ShowOptionsCommand { get; }
        ICommandModel AboutCommand { get; }
        ICommandModel HelpCommand { get; }
        ICommandModel CloseWindowCommand { get; }
        ICommandModel HelpTopicsCommand { get; }
        ICommandModel TemplatesViewCommand { get; }
        ICommandModel DuplicateTemplatesViewCommand { get; }
        ICommandModel CloseTabCommand { get; }
    }
}