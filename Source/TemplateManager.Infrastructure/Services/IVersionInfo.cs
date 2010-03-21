using System;

namespace TemplateManager.Infrastructure.Services
{
    public interface IVersionInfo
    {
        Version LatestVersion { get; }
        string InformationUrl { get; }
        string Configuration { get; }
        string Label { get; }
        string DownloadUrl { get; }
    }
}