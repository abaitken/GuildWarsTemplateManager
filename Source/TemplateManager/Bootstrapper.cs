using System.Windows;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.UnityExtensions;
using Microsoft.Practices.Unity;
using TemplateManager.Modules.Performance;
using TemplateManager.Modules.Services;
using TemplateManager.Modules.SkillsView;
using TemplateManager.ShellView;
using TemplateManager.Modules.Backup;

namespace TemplateManager
{
    internal class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IShellView, ShellView.ShellView>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IShellViewModel, ShellViewModel>();
        }

        protected override DependencyObject CreateShell()
        {
            var model = Container.Resolve<IShellViewModel>();
            model.ShowView();

            return model.View as DependencyObject;
        }

        protected override IModuleCatalog GetModuleCatalog()
        {
            var catalog = new ModuleCatalog();
            catalog.AddModule(typeof(BackupModule));
            catalog.AddModule(typeof(ServicesModule));
            catalog.AddModule(typeof(MainModule), "ServicesModule");
            catalog.AddModule(typeof(SkillsViewModule), "ServicesModule", "MainModule");
            catalog.AddModule(typeof(PerformanceModule));
            return catalog;
        }
    }
}