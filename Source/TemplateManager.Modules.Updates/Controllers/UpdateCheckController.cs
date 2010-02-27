using Microsoft.Practices.Unity;
using TemplateManager.Infrastructure;
using TemplateManager.Infrastructure.Controllers;
using TemplateManager.Modules.Updates.Presentation.UpdateCheck;

namespace TemplateManager.Modules.Updates.Controllers
{
    class UpdateCheckController : IUpdateCheckController
    {
        private readonly IUnityContainer container;

        public UpdateCheckController(IUnityContainer container)
        {
            this.container = container;
        }

        public void ShowDialog()
        {
            container.ShowViewDialog<IUpdateCheckViewModel>(model => model.View);
        }
    }
}
