using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using TemplateManager.Infrastructure;
using TemplateManager.Infrastructure.Model;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager
{
    internal class ServiceController : IServiceController
    {
        private readonly ISkillTemplateService service;
        private readonly IApplicationSettings applicationSettings;

        public ServiceController(ISkillTemplateService service, IApplicationSettings applicationSettings)
        {
            this.service = service;
            this.applicationSettings = applicationSettings;
            service.RefreshTemplates(BuildStore);
            applicationSettings.TemplateFolderChanged += TemplateFolderChanged;
        }

        #region IServiceController Members

        public string BuildStore
        {
            get { return applicationSettings.TemplateFolder; }
        }

        public ISkillTemplateService Service
        {
            get { return service; }
        }

        public event EventHandler TemplatesChanged;

        #endregion

        private void TemplateFolderChanged(object sender, EventArgs e)
        {
            service.RefreshTemplates(BuildStore);

            if(TemplatesChanged != null)
                TemplatesChanged(this, new EventArgs());
        }
    }
}