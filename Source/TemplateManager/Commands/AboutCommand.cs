using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using TemplateManager.AboutView;
using TemplateManager.Common.CommandModel;
using TemplateManager.ShellView;

namespace TemplateManager.Commands
{
    class AboutCommand : CommandModelBase
    {
        private readonly IUnityContainer container;

        public AboutCommand(IUnityContainer container)
        {
            this.container = container;
        }

        public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var view = container.Resolve<IAboutViewModel>().View;
            
            var owner = container.Resolve<IShellView>() as Window;

            if (owner != null)
                view.Owner = owner;

            view.ShowDialog();
        }
    }
}