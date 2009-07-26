using System;
using System.Windows.Input;
using TemplateManager.Common.CommandModel;

namespace TemplateManager.Commands
{
    class ActionParameterCommand<T> : CommandModelBase
    {
        private readonly Action<T> action;

        public ActionParameterCommand(Action<T> action)
        {
            this.action = action;
        }

        public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var parameter = (T)e.Parameter;

            action(parameter);

            e.Handled = true;
        }
    }
}