using System.Collections.Generic;
using System.Windows.Input;

namespace TemplateManager.Options
{
    public interface IOptionsViewModel
    {
        IOptionsView View { get; }

        void WriteSetings();

        string TemplateFolder { get; set; }
        bool IsTemplateFolderValid { get; }

        string ArchiveFolder { get; set; }
        bool IsDeleteBehaviourSettingValid { get; }

        string SelectedTheme { get; set; }

        string DeleteBehaviour { get; set; }
        
        IEnumerable<string> AvailableThemes { get; }
        IEnumerable<string> AvailableDeleteBehaviours { get; }


        string WindowTitle { get; }

        ICommand CloseWindowSuccessCommand { get; }
        ICommand CloseWindowCommand { get; }
        ICommand UseDefaultsCommand { get; }
        ICommand BrowseForArchiveFolderCommand { get; }
        ICommand BrowseForTemplateFolderCommand { get; }
    }
}