using System.Windows;
using InfiniteRain.Shared;
using InfiniteRain.Shared.Presentation.ViewManager;
using InfiniteRain.Shared.Services;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.UnityExtensions;
using Microsoft.Practices.Unity;
using TemplateManager.Infrastructure;
using TemplateManager.Modules.DataExplorer;
using TemplateManager.Modules.Services;
using TemplateManager.Modules.SkillsView;
using TemplateManager.Modules.Updates;
using TemplateManager.Modules.Workspace;
using TemplateManager.Presentation.Shell;
using TemplateManager.Services;

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
            Container.RegisterType<IApplicationInformationService, ApplicationInformation>();
            
            Container.RegisterType<ViewManager>(new ContainerControlledLifetimeManager());
        }

        protected override DependencyObject CreateShell()
        {
            var settings = Container.Resolve<IApplicationSettings>();
            settings.Reload();

            var arguments = CommandLineParser.Parse(args);

            if(arguments[Const.ResetOption])
                settings.Reset();

            var model = Container.Resolve<IMainWindowViewModel>();
            model.ShowView();

            return model.View as DependencyObject;
        }

        protected override IModuleCatalog GetModuleCatalog()
        {
            var catalog = new ModuleCatalog();
            var servicesModule = typeof(ServicesModule);
            var workspaceModule = typeof(WorkspaceModule);
            var skillsViewModule = typeof(SkillsViewModule);
            var dataExplorerModule = typeof(DataExplorerModule);
            var updatesModule = typeof(UpdatesModule);

            catalog.AddModule(servicesModule);
            catalog.AddModule(updatesModule);
            catalog.AddModule(skillsViewModule, servicesModule.Name);
            catalog.AddModule(dataExplorerModule, servicesModule.Name);
            catalog.AddModule(workspaceModule, servicesModule.Name, updatesModule.Name, dataExplorerModule.Name, skillsViewModule.Name);
            return catalog;
        }

        #region Nested type: Const

        private static class Const
        {
            public static readonly CommandLineOption ResetOption = new CommandLineOption("reset", "r");
        }

        #endregion
    }
}