using System;

namespace TemplateManager.Infrastructure.Services
{
    public interface IUpdateService
    {
        Version LatestVersion { get; }
        string InformationUrl { get; }
        void Refresh();
    }
}