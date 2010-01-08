namespace TemplateManager.Commands
{
    public class GenericCloseWindowCommand : CloseDialogWindowCommand
    {
        protected override bool DialogResult
        {
            get { return true; }
        }
    }
}