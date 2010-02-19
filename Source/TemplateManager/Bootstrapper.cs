using System.Windows;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.UnityExtensions;
using Microsoft.Practices.Unity;
using TemplateManager.Common;
using TemplateManager.Infrastructure;
using TemplateManager.Modules.DataExplorer;
using TemplateManager.Modules.Performance;
using TemplateManager.Modules.Services;
using TemplateManager.Modules.SkillsView;
using TemplateManager.ShellView;

namespace TemplateManager
{
    internal class Bootstrapper : UnityBootstrapper
    {
        private readonly string[] args;

        public Bootstrapper(string[] args)
        {
            this.args = args;
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IApplicationSettings, ApplicationSettings>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IMainWindowView, MainWindowView>();
            Container.RegisterType<IMainWindowViewModel, MainWindowViewModel>();
        }

        private static class Const
        {
            public static readonly CommandLineOption ResetOption = new CommandLineOption("reset", "r");
        }

        protected override DependencyObject CreateShell()
        {
            var settings = Container.Resolve<IApplicationSettings>();
            settings.Reload();

            var arguments = CommandLineParser.Parse(args);

            if (arguments[Const.ResetOption])
                settings.Reset();

            var model = Container.Resolve<IMainWindowViewModel>();
            model.ShowView();

            return model.View as DependencyObject;
        }

        protected override IModuleCatalog GetModuleCatalog()
        {
            var catalog = new ModuleCatalog();
            var servicesModule = typeof(ServicesModule);
            catalog.AddModule(servicesModule);
            var mainModule = typeof(MainModule);
            catalog.AddModule(mainModule, servicesModule.Name);
            catalog.AddModule(typeof(SkillsViewModule), servicesModule.Name, mainModule.Name);
            catalog.AddModule(typeof(DataExplorerModule), servicesModule.Name);
            catalog.AddModule(typeof(PerformanceModule));
            return catalog;
        }
    }
}