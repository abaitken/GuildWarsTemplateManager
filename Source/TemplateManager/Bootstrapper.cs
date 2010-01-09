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

            Container.RegisterType<IMainWindowView, ShellView.MainWindowView>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMainWindowViewModel, MainWindowViewModel>();
        }

        protected override DependencyObject CreateShell()
        {
            var model = Container.Resolve<IMainWindowViewModel>();
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