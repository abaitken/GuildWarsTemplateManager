using System.Windows;

namespace TemplateManager.Commands
{
    public abstract class CloseDialogWindowCommand : CloseWindowCommand
    {
        protected CloseDialogWindowCommand()
        {
        }

        protected CloseDialogWindowCommand(IQueryCancel queryCancel)
            : base(queryCancel)
        {
        }

        protected abstract bool DialogResult { get; }

        protected override void ExecuteImpl(Window window)
        {
            base.ExecuteImpl(window);
            window.DialogResult = DialogResult;
        }
    }
}