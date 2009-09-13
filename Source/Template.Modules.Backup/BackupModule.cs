using System;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;

namespace TemplateManager.Modules.Backup
{
    public class BackupModule : IModule
    {
        private readonly IUnityContainer container;

        public BackupModule(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            container.RegisterType<IBackupProvider, BackupProvider>();
        }
    }
}