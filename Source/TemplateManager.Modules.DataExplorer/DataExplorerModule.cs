using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using TemplateManager.Modules.DataExplorer.Presentation.DataExplorer;

namespace TemplateManager.Modules.DataExplorer
{
    public class DataExplorerModule : IModule
    {
        private readonly IUnityContainer container;

        public DataExplorerModule(IUnityContainer container)
        {
            this.container = container;
        }

        #region IModule Members

        public void Initialize()
        {
            container.RegisterType<IDataExplorerView, DataExplorerView>();
            container.RegisterType<IDataExplorerViewModel, DataExplorerViewModel>();
        }

        #endregion
    }
}