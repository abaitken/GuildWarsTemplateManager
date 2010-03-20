using System.Windows.Input;
using InfiniteRain.Shared.Presentation;

namespace TemplateManager.Modules.Workspace.Presentation.Options
{
    public interface IOptionsViewModel : IHeadedContent
    {
        IOptionsView View { get; }

        string TemplateFolder { get; set; }
        bool IsTemplateFolderValid { get; }

        ICommand ApplySettingsCommand { get; }
        ICommand UseDefaultsCommand { get; }
        ICommand BrowseForTemplateFolderCommand { get; }
        void WriteSettings();
    }
}