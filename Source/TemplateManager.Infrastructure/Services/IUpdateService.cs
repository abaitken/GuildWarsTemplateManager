namespace TemplateManager.Infrastructure.Services
{
    public interface IUpdateService
    {
        IVersionInfo GetLatestVersionInformation();
    }
}