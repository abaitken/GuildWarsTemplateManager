using System.Windows;
using System.Windows.Input;
using TemplateManager.Properties;

namespace TemplateManager.Commands
{
    internal class ResetSettingsCommand : GenericCloseWindowCommand
    {
        protected override bool DialogResult
        {
            get { return false; }
        }

        public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if(
                MessageBox.Show("Are you sure you want to restore the default settings?",
                                "Restore default settings",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            base.OnExecute(sender, e);

            Settings.Default.Reset();
        }
    }
}