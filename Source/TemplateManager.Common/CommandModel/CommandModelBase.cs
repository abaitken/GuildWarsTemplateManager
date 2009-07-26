using System;
using System.Windows.Input;

namespace TemplateManager.Common.CommandModel
{
    public abstract class CommandModelBase : ICommandModel
    {
        readonly RoutedCommand routedCommand;

        protected CommandModelBase()
            : this(new RoutedCommand()) { }

        protected CommandModelBase(RoutedCommand command)
        {
            if(command == null)
                throw new ArgumentNullException("command");

            routedCommand = command;
        }

        /// <summary>
        /// The routed command associated with this model
        /// </summary>
        public RoutedCommand Command
        {
            get { return routedCommand; }
        }

        /// <summary>
        /// Determines if a command is enabled. Override to provide custom behaviour.
        /// Do not call the base version when overriding.
        /// </summary>
        public virtual void OnQueryEnabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        /// <summary>
        /// Function to execute the command
        /// </summary>
        public abstract void OnExecute(object sender, ExecutedRoutedEventArgs e);
    }
}