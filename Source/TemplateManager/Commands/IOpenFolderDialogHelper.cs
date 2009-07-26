namespace TemplateManager.Commands
{
    public interface IOpenFolderDialogHelper
    {
        string GetCurrentFolder();
        void SetNewFolder(string folderPath);
    }
}