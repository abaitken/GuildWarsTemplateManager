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

        public void Initialize()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            container.RegisterType<IDataService, DataService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISkillTemplateService, SkillTemplateService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUpdateService, UpdateService>();
        }
    }
}