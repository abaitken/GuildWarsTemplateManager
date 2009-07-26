namespace TemplateManager.Commands
{
    abstract class OpenFolderDialogHelper : IOpenFolderDialogHelper
    {
        public abstract string GetCurrentFolder();
        public abstract void SetNewFolder(string folderPath);
    }
}