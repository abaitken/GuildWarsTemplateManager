namespace TemplateManager.Infrastructure.Model
{
    public interface IAttribute
    {
        int TemplateId { get; }
        string Name { get; }
        bool IsValid { get; }
        bool IsPrimaryOnly { get; }
    }
}