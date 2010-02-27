using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using TemplateManager.Infrastructure.Controllers;
using TemplateManager.Modules.Updates.Controllers;
using TemplateManager.Modules.Updates.Presentation.UpdateCheck;

namespace TemplateManager.Modules.Updates
{
    public class UpdatesModule : IModule
    {
        private readonly IUnityContainer container;

        public UpdatesModule(IUnityContainer container)
        {
            this.container = container;
        }

        #region IModule Members

        public void Initialize()
        {
            RegisterTypes();
        }

        #endregion

        private void RegisterTypes()
        {
            container.RegisterType<IUpdateCheckController, UpdateCheckController>();

            container.RegisterType<IUpdateCheckView, UpdateCheckView>();
            container.RegisterType<IUpdateCheckViewModel, UpdateCheckViewModel>();
        }
    }
}