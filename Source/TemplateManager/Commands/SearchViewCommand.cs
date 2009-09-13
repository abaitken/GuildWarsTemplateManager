using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using TemplateManager.SearchView;

namespace TemplateManager.Commands
{
    class SearchViewCommand : PushViewIntoRegionCommandBase
    {
        public SearchViewCommand(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager)
        {
        }

        protected override object View
        {
            get { return Container.Resolve<ISearchViewModel>().View; }
        }
    }
}