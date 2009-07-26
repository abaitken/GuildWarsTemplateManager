using System.Windows;
using System.Windows.Input;
using TemplateManager.Common.CommandModel;

namespace TemplateManager.Commands
{
    class HelpTopicsCommand : CommandModelBase
    {
        public override void OnQueryEnabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Help is unavailable");
        }
    }
}