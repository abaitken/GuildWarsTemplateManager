using Prism.Ioc;
using Prism.Modularity;
using TemperedSoftware.Shared.Presentation.ViewManager;
using TemplateManager.Infrastructure;
using TemplateManager.Modules.DataExplorer.Presentation.DataExplorer;
using Unity;

namespace TemplateManager.Modules.DataExplorer
{
    public class DataExplorerModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly ViewManager viewManager;

        public DataExplorerModule(IUnityContainer container, ViewManager viewManager)
        {
            this.container = container;
            this.viewManager = viewManager;
        }

        private void RegisterViews()
        {
            viewManager.Register(DataExplorerViewModel.ViewDetails,
                                 RegionNames.DocumentRegion,
                                 () => container.Resolve<IDataExplorerViewModel>().View);
        }

        private void RegisterTypes()
        {
            container.RegisterType<IDataExplorerView, DataExplorerView>();
            container.RegisterType<IDataExplorerViewModel, DataExplorerViewModel>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            RegisterTypes();
            RegisterViews();
        }
    }
}