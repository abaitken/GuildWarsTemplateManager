using System.Windows;
using System.Windows.Input;

namespace TemplateManager.Common.CommandModel
{
    public static class CreateCommandBinding
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommandModel),
                typeof(CreateCommandBinding),
                new PropertyMetadata(
                    new PropertyChangedCallback(OnCommandInvalidated)));

        public static ICommandModel GetCommand(DependencyObject sender)
        {
            return (ICommandModel)sender.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject sender, ICommandModel command)
        {
            sender.SetValue(CommandProperty, command);
        }

        static void OnCommandInvalidated(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            // clear existing bindings on the element we are attached to
            var element = (UIElement)dependencyObject;
            element.CommandBindings.Clear();

            // if given a command model, set up a binding
            var model = e.NewValue as ICommandModel;
            if (null != model)
            {
                element.CommandBindings.Add(new CommandBinding(model.Command,
                                                               model.OnExecute,
                                                               model.OnQueryEnabled));
            }

            // suggest to WPF to refresh commands
            CommandManager.InvalidateRequerySuggested();
        }
    }
}