using InfiniteRain.Shared.Presentation;
using Microsoft.Practices.Unity;
using TemplateManager.Infrastructure.Controllers;
using TemplateManager.Modules.Updates.Presentation.UpdateCheck;

namespace TemplateManager.Modules.Updates.Controllers
{
    internal class UpdateCheckController : IUpdateCheckController
    {
        private readonly IUnityContainer container;

        public UpdateCheckController(IUnityContainer container)
        {
            this.container = container;
        }

        #region IUpdateCheckController Members

        public void ShowDialog()
        {
            container.ShowViewDialog<IUpdateCheckViewModel>(model => model.View);
        }

        #endregion
    }
}