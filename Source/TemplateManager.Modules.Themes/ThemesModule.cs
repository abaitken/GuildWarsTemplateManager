using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;

namespace TemplateManager.Modules.Themes
{
    public class ThemesModule : IModule
    {
        private readonly IUnityContainer container;

        public ThemesModule(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            RegisterTypes();
            container.Resolve<IThemeManager>();
        }

        private void RegisterTypes()
        {
            container.RegisterType<IThemeManager, ThemeManager>(new ContainerControlledLifetimeManager());
        }
    }
}