namespace TemplateManager.Commands
{
    public class GenericCloseWindowCommand : CloseDialogWindowCommand
    {
        public GenericCloseWindowCommand()
        {
        }

        public GenericCloseWindowCommand(IQueryCancel queryCancel)
            : base(queryCancel)
        {
        }

        protected override bool DialogResult
        {
            get { return true; }
        }
    }
}