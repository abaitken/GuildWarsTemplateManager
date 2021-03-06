﻿using Prism.Commands;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using TemplateManager.Common;
using TemplateManager.Infrastructure;
using MessageBox=System.Windows.MessageBox;

namespace TemplateManager.Modules.Workspace.Presentation.Options
{
    public class OptionsViewModel : ViewModelBase, IOptionsViewModel
    {
        public static readonly ViewDetails ViewDetails = new ViewDetails("Options", ToolCategories.Tool);
        private readonly IApplicationSettings applicationSettings;
        private readonly IOptionsView view;
        private string templatefolder;

        public OptionsViewModel(IOptionsView view, IApplicationSettings applicationSettings)
        {
            this.view = view;
            this.applicationSettings = applicationSettings;
            view.Model = this;

            GenerateCommands();
            ReadSettings();
        }

        #region IOptionsViewModel Members

        public void WriteSettings()
        {
            if(applicationSettings.TemplateFolder != TemplateFolder)
                applicationSettings.TemplateFolder = TemplateFolder;

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

        public ICommand ApplySettingsCommand { get; private set; }
        public ICommand UseDefaultsCommand { get; private set; }
        public ICommand BrowseForTemplateFolderCommand { get; private set; }

        public IOptionsView View
        {
            get { return view; }
        }

        public string HeaderText
        {
            get { return ViewDetails.Name; }
        }

        #endregion

        private void GenerateCommands()
        {
            ApplySettingsCommand = new DelegateCommand(OnApply);
            UseDefaultsCommand = new DelegateCommand(OnResetSettings);
            BrowseForTemplateFolderCommand = new DelegateCommand(OnBrowseForTemplateFolder);
        }

        private void OnBrowseForTemplateFolder()
        {
            SelectNewFolder(() => TemplateFolder, v => TemplateFolder = v);
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

        private void OnResetSettings()
        {
            if(
                MessageBox.Show("Are you sure you want to restore the default settings?",
                                "Restore default settings",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            applicationSettings.Reset();
        }

        private void OnApply()
        {
            if(!IsTemplateFolderValid)
            {
                MessageBox.Show("Template folder is invalid.");
                return;
            }

            WriteSettings();
        }

        private void ReadSettings()
        {
            TemplateFolder = applicationSettings.TemplateFolder;
        }
    }
}