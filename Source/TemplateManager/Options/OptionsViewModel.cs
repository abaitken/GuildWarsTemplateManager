using System.Collections.Generic;
using System.IO;
using TemplateManager.Commands;
using TemplateManager.Common.CommandModel;
using TemplateManager.Common.ViewModel;
using TemplateManager.Properties;

namespace TemplateManager.Options
{
    public class OptionsViewModel : ViewModelBase, IOptionsViewModel
    {
        private readonly IOpenFolderDialogHelper archiveFolderDialogHelper;
        private readonly IOpenFolderDialogHelper templateFolderDialogHelper;
        private readonly IOptionsView view;
        private string archiveFolder;
        private string deleteBehaviour;
        private string templatefolder;

        public OptionsViewModel(IOptionsView view)
        {
            this.view = view;
            view.Model = this;

            CloseWindowSuccessCommand = new GenericCloseWindowCommand(this);
            CloseWindowCommand = new CancelOptionsWindowCommand();
            UseDefaultsCommand = new ResetSettingsCommand();
            OpenFolderDialogCommand = new OpenFolderDialogCommand();

            archiveFolderDialogHelper = new ArchiveFolderHelper(this);
            templateFolderDialogHelper = new TemplateFolderHelper(this);
            ReadSettings();
        }

        #region IOptionsViewModel Members

        public void WriteSetings()
        {
            if(Settings.Default.TemplateFolder != TemplateFolder)
                Settings.Default.TemplateFolder = TemplateFolder;

            if(Settings.Default.Theme != SelectedTheme)
                Settings.Default.Theme = SelectedTheme;

            Settings.Default.DeleteBehaviour = DeleteBehaviour;
            Settings.Default.ArchiveFolder = ArchiveFolder;
        }

        public string TemplateFolder
        {
            get { return templatefolder; }
            set
            {
                if(templatefolder == value)
                    return;

                templatefolder = value;
                SendPropertyChanged("TemplateFolder");
                SendPropertyChanged("IsTemplateFolderValid");
            }
        }

        public IOpenFolderDialogHelper TemplateFolderDialogHelper
        {
            get { return templateFolderDialogHelper; }
        }

        public bool IsTemplateFolderValid
        {
            get { return Directory.Exists(TemplateFolder); }
        }

        public string ArchiveFolder
        {
            get { return archiveFolder; }
            set
            {
                if(archiveFolder == value)
                    return;

                archiveFolder = value;
                SendPropertyChanged("ArchiveFolder");
                SendPropertyChanged("IsDeleteBehaviourSettingValid");
            }
        }

        public IEnumerable<string> AvailableDeleteBehaviours
        {
            get { return Infrastructure.DeleteBehaviour.Values; }
        }

        public string WindowTitle
        {
            get { return "Options"; }
        }

        public ICommandModel CloseWindowSuccessCommand { get; private set; }
        public ICommandModel CloseWindowCommand { get; private set; }
        public ICommandModel UseDefaultsCommand { get; private set; }
        public ICommandModel OpenFolderDialogCommand { get; private set; }

        public IOptionsView View
        {
            get { return view; }
        }


        public IOpenFolderDialogHelper ArchiveFolderDialogHelper
        {
            get { return archiveFolderDialogHelper; }
        }

        public bool IsDeleteBehaviourSettingValid
        {
            get
            {
                return DeleteBehaviour != Infrastructure.DeleteBehaviour.MoveAndArchive ||
                       (!string.IsNullOrEmpty(ArchiveFolder) && Directory.Exists(ArchiveFolder));
            }
        }


        public string SelectedTheme { get; set; }

        public string DeleteBehaviour
        {
            get { return deleteBehaviour; }
            set
            {
                if(deleteBehaviour == value)
                    return;

                deleteBehaviour = value;
                SendPropertyChanged("DeleteBehaviour");
                SendPropertyChanged("IsDeleteBehaviourSettingValid");
            }
        }

        public IEnumerable<string> AvailableThemes
        {
            get { return App.ThemeManager.AvailableThemes; }
        }

        public bool Validate()
        {
            return IsDeleteBehaviourSettingValid && IsTemplateFolderValid;
        }

        #endregion

        private void ReadSettings()
        {
            TemplateFolder = Settings.Default.TemplateFolder;
            if(string.IsNullOrEmpty(TemplateFolder))
                TemplateFolder = BuildStoreLocator.FindBuildStorePath();

            ArchiveFolder = Settings.Default.ArchiveFolder;
            DeleteBehaviour = Settings.Default.DeleteBehaviour;
            SelectedTheme = Settings.Default.Theme;
        }
    }
}