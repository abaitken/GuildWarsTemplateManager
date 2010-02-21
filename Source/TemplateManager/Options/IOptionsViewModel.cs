using System.Windows.Input;
using TemplateManager.Infrastructure.Interfaces;

namespace TemplateManager.Options
{
    public interface IOptionsViewModel : IHeadedContent
    {
        IOptionsView View { get; }

        string TemplateFolder { get; set; }
        bool IsTemplateFolderValid { get; }

        ICommand ApplySettingsCommand { get; }
        ICommand UseDefaultsCommand { get; }
        ICommand BrowseForTemplateFolderCommand { get; }
        void WriteSetings();
    }
}