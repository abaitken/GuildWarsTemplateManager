namespace TemplateManager.ShellView
{
    public interface IShellView
    {
        IShellViewModel Model { get; set; }
        void Show();
    }
}