namespace TemplateManager.ShellView
{
    public interface IMainWindowView
    {
        IMainWindowViewModel Model { get; set; }
        void Show();
    }
}