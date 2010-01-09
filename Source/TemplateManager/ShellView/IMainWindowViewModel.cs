namespace TemplateManager.ShellView
{
    public interface IMainWindowViewModel
    {
        IMainWindowView View { get; }
        string WindowTitle { get; }
        void ShowView();

    }
}