namespace TemplateManager.Presentation.Shell
{
    public interface IMainWindowView
    {
        IMainWindowViewModel Model { get; set; }
        void Show();
    }
}