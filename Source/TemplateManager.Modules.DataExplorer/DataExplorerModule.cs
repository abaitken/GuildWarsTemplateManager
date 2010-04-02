using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using TemperedSoftware.Shared.Presentation.ViewManager;
using TemplateManager.Infrastructure;
using TemplateManager.Modules.DataExplorer.Presentation.DataExplorer;

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

        #region IModule Members

        public void Initialize()
        {
            RegisterTypes();
            RegisterViews();
        }

        #endregion

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
    }
}