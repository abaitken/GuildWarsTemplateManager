using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using TemplateManager.Modules.SkillsView.DuplicateTemplate;
using TemplateManager.Modules.SkillsView.SkillView;

namespace TemplateManager.Modules.SkillsView
{
    public class SkillsViewModule : IModule
    {
        private readonly IUnityContainer container;

        public SkillsViewModule(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            RegisterViews();
        }

        private void RegisterViews()
        {
            container.RegisterType<ISkillsView, SkillView.SkillsView>();
            container.RegisterType<ISkillsViewModel, SkillsViewModel>();

            container.RegisterType<IDuplicateSkillTemplateView, DuplicateSkillTemplateView>();
            container.RegisterType<IDuplicateSkillTemplateViewModel, DuplicateSkillTemplateViewModel>();
        }
    }
}