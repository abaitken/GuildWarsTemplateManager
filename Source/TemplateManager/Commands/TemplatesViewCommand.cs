using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using TemplateManager.Modules.SkillsView.SkillView;

namespace TemplateManager.Commands
{
    class TemplatesViewCommand : PushViewIntoRegionCommandBase
    {
        public TemplatesViewCommand(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager)
        {
        }

        protected override object View
        {
            get { return Container.Resolve<ISkillsViewModel>().View; }
        }
    }
}