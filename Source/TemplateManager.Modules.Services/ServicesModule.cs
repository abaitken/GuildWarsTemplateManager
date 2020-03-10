using Prism.Ioc;
using Prism.Modularity;
using TemplateManager.Infrastructure.Services;
using Unity;
using Unity.Lifetime;

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
        
        public void OnInitialized(IContainerProvider containerProvider)
        {
            RegisterTypes();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        #endregion

        private void RegisterTypes()
        {
            container.RegisterType<IDataService, DataService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISkillTemplateService, SkillTemplateService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUpdateService, UpdateService>();
        }
    }
}