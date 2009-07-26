using System;
using System.Windows.Input;
using TemplateManager.Common.CommandModel;

namespace TemplateManager.Commands
{
    class ActionCommand : CommandModelBase
    {
        private readonly Action action;

        public ActionCommand(Action action)
        {
            this.action = action;
        }

        public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            action();

            e.Handled = true;
        }
    }
}