using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using TemplateManager.AboutView;
using TemplateManager.Infrastructure;
using TemplateManager.Infrastructure.Services;
using TemplateManager.MainView;
using TemplateManager.Options;
using TemplateManager.UpdateCheck;

namespace TemplateManager
{
    internal class MainModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public MainModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        #region IModule Members

        public void Initialize()
        {
            RegisterViews();
            RegisterViewsWithRegions();
        }

        #endregion

        private void RegisterViewsWithRegions()
        {
            regionManager.RegisterViewWithRegion(RegionNames.ShellRegion, () => container.Resolve<IMainViewModel>().View);
        }

        private void RegisterViews()
        {
            container.RegisterType<IMainView, MainView.MainView>();
            container.RegisterType<IMainViewModel, MainViewModel>();

            container.RegisterType<IAboutView, AboutDialog>();
            container.RegisterType<IAboutViewModel, AboutViewModel>();

            container.RegisterType<IOptionsView, OptionsWindow>();
            container.RegisterType<IOptionsViewModel, OptionsViewModel>();

            container.RegisterType<IUpdateCheckView, UpdateCheckView>();
            container.RegisterType<IUpdateCheckViewModel, UpdateCheckViewModel>();

            container.RegisterType<IServiceController, ServiceController>(new ContainerControlledLifetimeManager());
        }
    }
}