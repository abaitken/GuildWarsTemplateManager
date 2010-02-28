using System;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using TemplateManager.Infrastructure;
using TemplateManager.Infrastructure.Controllers;
using TemplateManager.Modules.DataExplorer.Presentation.DataExplorer;

namespace TemplateManager.Modules.DataExplorer
{
    public class DataExplorerModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IViewManager viewManager;

        public DataExplorerModule(IUnityContainer container, IViewManager viewManager)
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

        private void RegisterViews()
        {
            viewManager.Register(DataExplorerViewModel.ViewDetails,
                                 RegionNames.DocumentRegion,
                                 () => container.Resolve<IDataExplorerViewModel>().View);
        }

        #endregion

        private void RegisterTypes()
        {
            container.RegisterType<IDataExplorerView, DataExplorerView>();
            container.RegisterType<IDataExplorerViewModel, DataExplorerViewModel>();
        }
    }
}