using System;
using System.Windows;
using System.Windows.Input;
using TemplateManager.Common.CommandModel;

namespace TemplateManager.Commands
{
    public abstract class CloseWindowCommand : CommandModelBase
    {
        public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var window = e.Parameter as Window;

            if(window == null)
                throw new ArgumentException("Expected parameter to be of type 'Window'");

            ExecuteImpl(window);
            window.Close();
            
            e.Handled = true;
        }

        protected virtual void ExecuteImpl(Window window)
        {
            // Left blank intentionally
        }
    }
}