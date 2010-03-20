using InfiniteRain.Shared.Presentation.ViewManager;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using TemplateManager.Infrastructure;
using TemplateManager.Infrastructure.Controllers;
using TemplateManager.Modules.SkillsView.DuplicateTemplate;
using TemplateManager.Modules.SkillsView.SkillView;

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

        #region IModule Members

        public void Initialize()
        {
            RegisterTypes();
            RegisterViews();
        }

        #endregion

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
    }
}