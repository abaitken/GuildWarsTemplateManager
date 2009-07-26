namespace TemplateManager.Options
{
    class ArchiveFolderHelper : OptionsFolderHelperBase
    {
        public ArchiveFolderHelper(IOptionsViewModel viewModel)
            : base(viewModel)
        {
        }

        public override string GetCurrentFolder()
        {
            return viewModel.ArchiveFolder;
        }

        public override void SetNewFolder(string folderPath)
        {
            viewModel.ArchiveFolder = folderPath;
        }
    }
}