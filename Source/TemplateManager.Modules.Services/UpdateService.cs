using System;
using TemplateManager.Infrastructure.Services;
using TemplateManager.Modules.Services.RemoteServices;

namespace TemplateManager.Modules.Services
{
    internal class UpdateService : IUpdateService
    {
        #region IUpdateService Members

        public IVersionInfo GetLatestVersionInformation()
        {
            var client = new TemplateManagerService();

            var info = client.GetLatestVersion();

            if(info == null)
                return null;

            var result = new UpdateInfo
                             {
                                 LatestVersion = new Version(info.Major, info.Minor, info.Build, info.Revision),
                                 InformationUrl = info.InformationUrl,
                                 Configuration = info.Configuration,
                                 DownloadUrl = info.DownloadUrl,
                                 Label = info.Label
                             };

            return result;
        }

        #endregion

        #region Nested type: UpdateInfo

        private class UpdateInfo : IVersionInfo
        {
            #region IVersionInfo Members

            public Version LatestVersion { get; set; }
            public string InformationUrl { get; set; }
            public string Configuration { get; set; }
            public string Label { get; set; }
            public string DownloadUrl { get; set; }

            #endregion
        }

        #endregion
    }
}