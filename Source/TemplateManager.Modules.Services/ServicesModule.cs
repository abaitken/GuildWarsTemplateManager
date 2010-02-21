using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.Modules.Services
{
    public class ServicesModule : IModule
    {
        private readonly IUnityContainer container;

        public ServicesModule(IUnityContainer container)
        {
            this.container = container;
        }

        #region IModule Members

        public void Initialize()
        {
            RegisterServices();
        }

        #endregion

        private void RegisterServices()
        {
            container.RegisterType<IDataService, DataService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISkillTemplateService, SkillTemplateService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUpdateService, UpdateService>();
        }
    }
}