using System;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace TemplateManager.Common.Commands
{
    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action executeMethod)
            : base(i => executeMethod())
        {
        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
            : base(i => executeMethod(), i => canExecuteMethod())
        {
        }
    }
}