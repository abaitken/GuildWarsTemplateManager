using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using TemplateManager.Common.ViewModel;
using TemplateManager.Infrastructure;
using TemplateManager.Modules.Themes;
using MessageBox=System.Windows.MessageBox;

namespace TemplateManager.Options
{
    public class OptionsViewModel : ViewModelBase, IOptionsViewModel
    {
        private readonly IOptionsView view;
        private readonly IApplicationSettings applicationSettings;
        private readonly IThemeManager themeManager;
        private string archiveFolder;
        private string deleteBehaviour;
        private string templatefolder;

        public OptionsViewModel(IOptionsView view, IApplicationSettings applicationSettings, IThemeManager themeManager)
        {
            this.view = view;
            this.applicationSettings = applicationSettings;
            this.themeManager = themeManager;
            view.Model = this;

            GenerateCommands();
            ReadSettings();
        }

        #region IOptionsViewModel Members

        public void WriteSetings()
        {
            if (applicationSettings.TemplateFolder != TemplateFolder)
                applicationSettings.TemplateFolder = TemplateFolder;

            if(applicationSettings.Theme != SelectedTheme)
                applicationSettings.Theme = SelectedTheme;

            applicationSettings.DeleteBehaviour = DeleteBehaviour;
            applicationSettings.ArchiveFolder = ArchiveFolder;
            applicationSettings.Save();
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

        public ICommand CloseWindowSuccessCommand { get; private set; }
        public ICommand CloseWindowCommand { get; private set; }
        public ICommand UseDefaultsCommand { get; private set; }
        public ICommand BrowseForArchiveFolderCommand { get; private set; }
        public ICommand BrowseForTemplateFolderCommand { get; private set; }

        public IOptionsView View
        {
            get { return view; }
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
            get { return themeManager.AvailableThemes; }
        }

        #endregion

        private void GenerateCommands()
        {
            CloseWindowSuccessCommand = new DelegateCommand<Window>(OnOK);
            CloseWindowCommand = new DelegateCommand<Window>(OnCancel);
            UseDefaultsCommand = new DelegateCommand<Window>(OnResetSettings);
            BrowseForArchiveFolderCommand = new DelegateCommand<object>(OnBrowseForArchiveFolder);
            BrowseForTemplateFolderCommand = new DelegateCommand<object>(OnBrowseForTemplateFolder);
        }

        private void OnBrowseForTemplateFolder(object obj)
        {
            SelectNewFolder(() => TemplateFolder, v => TemplateFolder = v);
        }

        private void OnBrowseForArchiveFolder(object obj)
        {
            SelectNewFolder(() => ArchiveFolder, v => ArchiveFolder = v);
        }

        private static void SelectNewFolder(Func<string> get, Action<string> set)
        {
            var result = SelectNewFolder(get());

            if(!string.IsNullOrEmpty(result))
                set(result);
        }

        private static string SelectNewFolder(string currentFolder)
        {
            var folder = currentFolder;

            if(string.IsNullOrEmpty(folder) || !Directory.Exists(folder))
                folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var fd = new FolderBrowserDialog
                         {
                             SelectedPath = folder
                         };

            return fd.ShowDialog() == DialogResult.OK ? fd.SelectedPath : null;
        }

        private void OnResetSettings(Window obj)
        {
            if(
                MessageBox.Show("Are you sure you want to restore the default settings?",
                                "Restore default settings",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            CloseWindow(obj, false);
            applicationSettings.Reset();
        }

        private void OnOK(Window obj)
        {
            if(!IsDeleteBehaviourSettingValid || !IsTemplateFolderValid)
                return;

            CloseWindow(obj, true);
        }

        private static void CloseWindow(Window obj, bool result)
        {
            obj.DialogResult = result;
            obj.Close();
        }

        private static void OnCancel(Window obj)
        {
            CloseWindow(obj, false);
        }

        private void ReadSettings()
        {
            TemplateFolder = applicationSettings.TemplateFolder;
            ArchiveFolder = applicationSettings.ArchiveFolder;
            DeleteBehaviour = applicationSettings.DeleteBehaviour;
            SelectedTheme = applicationSettings.Theme;
        }
    }
}