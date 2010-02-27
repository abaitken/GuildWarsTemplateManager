namespace TemplateManager.Presentation.Shell
{
    public interface IMainWindowViewModel
    {
        IMainWindowView View { get; }
        string WindowTitle { get; }
        void ShowView();
    }
}