namespace TemplateManager.Commands
{
    public class CancelOptionsWindowCommand : CloseDialogWindowCommand
    {
        protected override bool DialogResult
        {
            get { return false; }
        }
    }
}