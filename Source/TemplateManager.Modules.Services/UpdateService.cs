using System;
using TemplateManager.Infrastructure.Services;
using TemplateManager.Modules.Services.RemoteServices;

namespace TemplateManager.Modules.Services
{
    internal class UpdateService : IUpdateService
    {
        private bool cacheInformation;
        private string informationUrl;
        private Version latestVersion;

        #region IUpdateService Members

        public Version LatestVersion
        {
            get
            {
                GetInformation();
                return latestVersion;
            }
        }

        public string InformationUrl
        {
            get
            {
                GetInformation();
                return informationUrl;
            }
        }

        public void Refresh()
        {
            cacheInformation = false;
        }

        #endregion

        private void GetInformation()
        {
            if(cacheInformation)
                return;

            cacheInformation = true;

            var client = new TemplateManagerService();
            var result = client.GetLatestVersion();

            if(result == null)
                return;

            latestVersion = new Version(result.Major, result.Minor, result.Build, result.Revision);
            informationUrl = result.InformationUrl;
        }
    }
}