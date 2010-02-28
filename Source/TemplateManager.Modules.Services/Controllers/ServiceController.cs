using System;
using TemplateManager.Infrastructure;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.Modules.Services.Controllers
{
    internal class ServiceController : IServiceController
    {
        private readonly IApplicationSettings applicationSettings;
        private readonly ISkillTemplateService service;

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