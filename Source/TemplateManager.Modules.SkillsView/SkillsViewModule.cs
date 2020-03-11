using Prism.Ioc;
using Prism.Modularity;
using TemplateManager.Common;
using TemplateManager.Infrastructure;
using TemplateManager.Modules.SkillsView.DuplicateTemplate;
using TemplateManager.Modules.SkillsView.SkillView;
using Unity;

namespace TemplateManager.Modules.SkillsView
{
    public class SkillsViewModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly ViewManager viewManager;

        public SkillsViewModule(IUnityContainer container, ViewManager viewManager)
        {
            this.container = container;
            this.viewManager = viewManager;
        }

        private void RegisterViews()
        {
            viewManager.Register(DuplicateSkillTemplateViewModel.ViewDetails,
                                 RegionNames.DocumentRegion,
                                 () => container.Resolve<IDuplicateSkillTemplateViewModel>().View);

            viewManager.Register(SkillsViewModel.ViewDetails,
                                 RegionNames.DocumentRegion,
                                 () => container.Resolve<ISkillsViewModel>().View);
        }

        private void RegisterTypes()
        {
            container.RegisterType<ISkillsView, SkillView.SkillsView>();
            container.RegisterType<ISkillsViewModel, SkillsViewModel>();

            container.RegisterType<IDuplicateSkillTemplateView, DuplicateSkillTemplateView>();
            container.RegisterType<IDuplicateSkillTemplateViewModel, DuplicateSkillTemplateViewModel>();
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