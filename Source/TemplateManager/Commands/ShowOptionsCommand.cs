using System.Windows.Input;
using Microsoft.Practices.Unity;
using TemplateManager.Common.CommandModel;
using TemplateManager.Options;

namespace TemplateManager.Commands
{
    public class ShowOptionsCommand : CommandModelBase
    {
        private readonly IUnityContainer container;

        public ShowOptionsCommand(IUnityContainer container)
        {
            this.container = container;
        }

        public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var window = container.Resolve<IOptionsViewModel>().View;
            var windowResult = window.ShowDialog();

            if (windowResult.HasValue && windowResult.Value)
                window.Model.WriteSetings();
        }
    }
}