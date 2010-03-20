using System.Windows.Input;
using InfiniteRain.Shared.Presentation;

namespace TemplateManager.Modules.Workspace.Presentation.Options
{
    public interface IOptionsViewModel
    {
        IOptionsView View { get; }

        string TemplateFolder { get; set; }
        bool IsTemplateFolderValid { get; }

        ICommand ApplySettingsCommand { get; }
        ICommand UseDefaultsCommand { get; }
        ICommand BrowseForTemplateFolderCommand { get; }
        string HeaderText { get; }
        void WriteSettings();
    }
}