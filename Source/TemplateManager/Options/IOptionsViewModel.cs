using System.Collections.Generic;
using TemplateManager.Commands;
using TemplateManager.Common.CommandModel;

namespace TemplateManager.Options
{
    public interface IOptionsViewModel : IQueryCancel
    {
        IOptionsView View { get; }

        void WriteSetings();

        string TemplateFolder { get; set; }
        IOpenFolderDialogHelper TemplateFolderDialogHelper { get; }
        bool IsTemplateFolderValid { get; }

        string ArchiveFolder { get; set; }
        IOpenFolderDialogHelper ArchiveFolderDialogHelper { get; }
        bool IsDeleteBehaviourSettingValid { get; }

        string SelectedTheme { get; set; }

        string DeleteBehaviour { get; set; }
        
        IEnumerable<string> AvailableThemes { get; }
        IEnumerable<string> AvailableDeleteBehaviours { get; }


        string WindowTitle { get; }

        ICommandModel CloseWindowSuccessCommand { get; }
        ICommandModel CloseWindowCommand { get; }
        ICommandModel UseDefaultsCommand { get; }
        ICommandModel OpenFolderDialogCommand { get; }
    }
}