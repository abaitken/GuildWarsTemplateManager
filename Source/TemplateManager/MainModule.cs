using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using TemplateManager.AboutView;
using TemplateManager.Infrastructure;
using TemplateManager.Infrastructure.Services;
using TemplateManager.Options;
using TemplateManager.SearchView;

namespace TemplateManager
{
    class MainModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public MainModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            RegisterViews();
            RegisterViewsWithRegions();
        }

        private void RegisterViewsWithRegions()
        {
            regionManager.RegisterViewWithRegion(RegionNames.SidePanelRegion,
                                                 () => container.Resolve<ISearchViewModel>().View);
        }

        private void RegisterViews()
        {
            container.RegisterType<IAboutView, AboutDialog>();
            container.RegisterType<IAboutViewModel, AboutViewModel>();

            container.RegisterType<ISearchView, SearchPanel>();
            container.RegisterType<ISearchViewModel, SearchViewModel>();

            container.RegisterType<IOptionsView, OptionsWindow>();
            container.RegisterType<IOptionsViewModel, OptionsViewModel>();

            container.RegisterType<IServiceController, ServiceController>(new ContainerControlledLifetimeManager());
        }
    }
}