using System.Windows.Input;

namespace TemplateManager.Common.CommandModel
{
    public interface ICommandModel
    {
        RoutedCommand Command { get; }
        void OnQueryEnabled(object sender, CanExecuteRoutedEventArgs e);
        void OnExecute(object sender, ExecutedRoutedEventArgs e);
    }
}