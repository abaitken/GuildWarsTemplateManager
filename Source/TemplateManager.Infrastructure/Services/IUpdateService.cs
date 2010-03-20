using System;

namespace TemplateManager.Infrastructure.Services
{
    public interface IUpdateService
    {
        IVersionInfo GetLatestVersionInformation();
    }

    public interface IVersionInfo
    {
        Version LatestVersion { get; }
        string InformationUrl { get; }
    }
}