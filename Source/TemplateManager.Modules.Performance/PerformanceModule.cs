using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

namespace TemplateManager.Modules.Performance
{
    public class PerformanceModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public PerformanceModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        #region IModule Members

        public void Initialize()
        {
            //container.RegisterType<IPerformanceView, PerformanceView>();
            //container.RegisterType<IPerformanceViewModel, PerformanceViewModel>();

            //regionManager.RegisterViewWithRegion(RegionNames.SidePanelRegion,
            //                                     () => container.Resolve<IPerformanceViewModel>().View);
        }

        #endregion
    }
}