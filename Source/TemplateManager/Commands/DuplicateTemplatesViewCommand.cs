using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using TemplateManager.Modules.SkillsView.DuplicateTemplate;

namespace TemplateManager.Commands
{
    class DuplicateTemplatesViewCommand : PushViewIntoRegionCommandBase
    {
        public DuplicateTemplatesViewCommand(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager)
        {
        }

        protected override object View
        {
            get { return Container.Resolve<IDuplicateSkillTemplateViewModel>().View; }
        }
    }
}