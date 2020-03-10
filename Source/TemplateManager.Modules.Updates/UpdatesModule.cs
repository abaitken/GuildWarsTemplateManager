using Prism.Ioc;
using Prism.Modularity;
using TemplateManager.Infrastructure.Controllers;
using TemplateManager.Modules.Updates.Controllers;
using TemplateManager.Modules.Updates.Presentation.UpdateCheck;
using Unity;

namespace TemplateManager.Modules.Updates
{
    public class UpdatesModule : IModule
    {
        private readonly IUnityContainer container;

        public UpdatesModule(IUnityContainer container)
        {
            this.container = container;
        }


        public void OnInitialized(IContainerProvider containerProvider)
        {
            RegisterTypes();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }


        private void RegisterTypes()
        {
            container.RegisterType<IUpdateCheckController, UpdateCheckController>();

            container.RegisterType<IUpdateCheckView, UpdateCheckView>();
            container.RegisterType<IUpdateCheckViewModel, UpdateCheckViewModel>();
        }
    }
}