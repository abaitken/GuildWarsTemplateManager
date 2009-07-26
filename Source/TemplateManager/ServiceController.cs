using System;
using System.ComponentModel;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using TemplateManager.Infrastructure;
using TemplateManager.Infrastructure.Model;
using TemplateManager.Infrastructure.Services;
using TemplateManager.Properties;

namespace TemplateManager
{
    internal class ServiceController : IServiceController
    {
        private readonly ISkillTemplateService service;

        public ServiceController(ISkillTemplateService service)
        {
            this.service = service;
            service.RefreshTemplates(BuildStore);
            Settings.Default.PropertyChanged += PropertyChanged;
        }

        #region IServiceController Members

        public string BuildStore
        {
            get { return Settings.Default.TemplateFolder; }
        }

        public ISkillTemplateService Service
        {
            get { return service; }
        }

        public event EventHandler TemplatesChanged;

        #endregion

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "TemplateFolder")
                OnBuildStoreChanged();
        }

        private void OnBuildStoreChanged()
        {
            service.RefreshTemplates(BuildStore);

            if(TemplatesChanged != null)
                TemplatesChanged(this, new EventArgs());

        }

        public void DeleteTemplate(SkillTemplate template)
        {
            switch (Settings.Default.DeleteBehaviour)
            {
                case DeleteBehaviour.DeleteAndRecycle:
                    FileSystem.DeleteFile(template.BuildFile, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
                    break;
                case DeleteBehaviour.MoveAndArchive:
                    var targetPath = Path.Combine(Settings.Default.ArchiveFolder, Path.GetFileName(template.BuildFile));
                    FileSystem.MoveFile(template.BuildFile, targetPath, UIOption.AllDialogs, UICancelOption.DoNothing);
                    break;
                case DeleteBehaviour.DeletePermanently:
                    FileSystem.DeleteFile(template.BuildFile, UIOption.AllDialogs, RecycleOption.DeletePermanently);
                    break;
                default:
                    throw new InvalidOperationException("Unknown delete behaviour");
            }

            if (TemplatesChanged != null)
                TemplatesChanged(this, new EventArgs());
        }
    }
}