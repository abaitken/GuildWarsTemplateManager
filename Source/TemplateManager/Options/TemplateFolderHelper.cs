namespace TemplateManager.Options
{
    class TemplateFolderHelper : OptionsFolderHelperBase
    {
        public TemplateFolderHelper(IOptionsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override string GetCurrentFolder()
        {
            return viewModel.TemplateFolder;
        }

        public override void SetNewFolder(string folderPath)
        {
            viewModel.TemplateFolder = folderPath;
        }
    }
}