using System;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using TemplateManager.Commands;
using TemplateManager.Common.CommandModel;
using TemplateManager.Infrastructure.Services;
using TemplateManager.UpdateCheck;
using TemplateManager.AboutView;
using TemplateManager.ShellView;
using System.Windows;
using TemplateManager.Options;

namespace TemplateManager.MainView
{
    class MainViewModel : IMainViewModel
    {
        private readonly IApplicationInformationService applicationInformationService;
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;
        private readonly IUpdateService updateService;
        private readonly IMainView view;

        public MainViewModel(IMainView view,
                              IUpdateService updateService,
                              IApplicationInformationService applicationInformationService,
                              IUnityContainer container,
                              IRegionManager regionManager)
        {
            this.view = view;
            this.updateService = updateService;
            this.applicationInformationService = applicationInformationService;
            this.container = container;
            this.regionManager = regionManager;
            view.Model = this;

            GenerateCommands();
        }

        #region IShellViewModel Members

        public IMainView View
        {
            get { return view; }
        }

        public ICommand ShowOptionsCommand { get; private set; }
        public ICommand AboutCommand { get; private set; }
        public ICommand HelpCommand { get; private set; }
        public ICommand CloseWindowCommand { get; private set; }
        public ICommand HelpTopicsCommand { get; private set; }
        public ICommandModel TemplatesViewCommand { get; private set; }
        public ICommandModel DuplicateTemplatesViewCommand { get; private set; }
        public ICommandModel CloseTabCommand { get; private set; }
        public ICommand ShowUpdateCheckWindowCommand { get; private set; }

        public void OnViewLoaded()
        {
            CheckForUpdates();
        }

        #endregion

        private void CheckForUpdates()
        {
            var currentVersion = new Version(applicationInformationService.FileVersion);
            var latestVersion = updateService.LatestVersion;

            if(currentVersion >= latestVersion)
                return;

            DisplayUpdateNotification();
        }

        private void GenerateCommands()
        {
            ShowOptionsCommand = new DelegateCommand<object>(ShowOptionsWindow);
            HelpCommand = new DelegateCommand<object>(OnHelpRequested);
            AboutCommand = new DelegateCommand<object>(ShowAboutWindow);
            CloseWindowCommand = new DelegateCommand<Window>(OnCloseWindow);
            HelpTopicsCommand = new DelegateCommand<object>(OnHelpTopicsRequested);
            TemplatesViewCommand = new TemplatesViewCommand(container, regionManager);
            DuplicateTemplatesViewCommand = new DuplicateTemplatesViewCommand(container, regionManager);
            CloseTabCommand = new CloseTabCommand(regionManager);
            ShowUpdateCheckWindowCommand = new DelegateCommand<object>(DisplayUpdateNotification);
        }

        private static void OnCloseWindow(Window obj)
        {
            obj.Close();
        }

        private static void OnHelpRequested(object obj)
        {
            MessageBox.Show("Help is unavailable");
        }

        private static void OnHelpTopicsRequested(object obj)
        {
            MessageBox.Show("Help topics are unavailable");
        }

        private void DisplayUpdateNotification(object obj)
        {
            DisplayUpdateNotification();
        }

        private void DisplayUpdateNotification()
        {
            var updateCheckView = container.Resolve<IUpdateCheckViewModel>().View;
            updateCheckView.Display();
        }

        private void ShowAboutWindow(object obj)
        {
            var view = container.Resolve<IAboutViewModel>().View;

            // TODO : Fix owner
            var owner = container.Resolve<IShellView>() as Window;

            if (owner != null)
                view.Owner = owner;

            view.ShowDialog();
        }

        private void ShowOptionsWindow(object obj)
        {
            var window = container.Resolve<IOptionsViewModel>().View;
            var windowResult = window.ShowDialog();

            if (windowResult.HasValue && windowResult.Value)
                window.Model.WriteSetings();
        }
    }

}
