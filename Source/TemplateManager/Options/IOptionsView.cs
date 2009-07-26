namespace TemplateManager.Options
{
    public interface IOptionsView
    {
        IOptionsViewModel Model { get; set; }
        bool? ShowDialog();
    }
}