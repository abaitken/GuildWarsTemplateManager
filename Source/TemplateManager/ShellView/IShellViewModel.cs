using TemplateManager.Common.CommandModel;

namespace TemplateManager.ShellView
{
    public interface IShellViewModel
    {
        IShellView View { get; }
        string WindowTitle { get; }
        void ShowView();

    }
}