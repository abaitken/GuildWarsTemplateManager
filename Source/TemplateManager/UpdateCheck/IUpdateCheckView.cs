namespace TemplateManager.UpdateCheck
{
    public interface IUpdateCheckView
    {
        IUpdateCheckViewModel Model { get; set; }
        void Display();
    }
}