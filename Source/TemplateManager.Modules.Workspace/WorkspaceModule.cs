using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using TemplateManager.Common;
using TemplateManager.Infrastructure;
using TemplateManager.Modules.Workspace.Presentation.About;
using TemplateManager.Modules.Workspace.Presentation.Options;
using TemplateManager.Modules.Workspace.Presentation.Workspace;
using Unity;

namespace TemplateManager.Modules.Workspace
{
    public class WorkspaceModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;
        private readonly ViewManager viewManager;

        public WorkspaceModule(IUnityContainer container, IRegionManager regionManager, ViewManager viewManager)
        {
            this.container = container;
            this.regionManager = regionManager;
            this.viewManager = viewManager;
        }
        
        private void RegisterViewsWithRegions()
        {
            regionManager.RegisterViewWithRegion(RegionNames.ShellRegion,
                                                 () => container.Resolve<IWorkspaceViewModel>().View);
            viewManager.Register(OptionsViewModel.ViewDetails,
                                 RegionNames.DocumentRegion,
                                 () => container.Resolve<IOptionsViewModel>().View);
        }

        private void RegisterViews()
        {
            container.RegisterType<IAboutView, AboutView>();
            container.RegisterType<IAboutViewModel, AboutViewModel>();

            container.RegisterType<IOptionsView, OptionsView>();
            container.RegisterType<IOptionsViewModel, OptionsViewModel>();

            container.RegisterType<IWorkspaceView, WorkspaceView>();
            container.RegisterType<IWorkspaceViewModel, WorkspaceViewModel>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            RegisterViews();
            RegisterViewsWithRegions();
        }
    }
}