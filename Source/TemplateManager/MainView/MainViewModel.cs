using System;
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

            GenerateCommands(container);
        }

        #region IShellViewModel Members

        public IMainView View
        {
            get { return view; }
        }

        public ICommandModel ShowOptionsCommand { get; private set; }
        public ICommandModel AboutCommand { get; private set; }
        public ICommandModel HelpCommand { get; private set; }
        public ICommandModel CloseWindowCommand { get; private set; }
        public ICommandModel HelpTopicsCommand { get; private set; }
        public ICommandModel TemplatesViewCommand { get; private set; }
        public ICommandModel DuplicateTemplatesViewCommand { get; private set; }
        public ICommandModel CloseTabCommand { get; private set; }
        public ICommandModel ShowUpdateCheckWindowCommand { get; private set; }

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

        private void GenerateCommands(IUnityContainer container)
        {
            ShowOptionsCommand = new ActionCommand(ShowOptionsWindow);
            HelpCommand = new HelpTopicsCommand();
            AboutCommand = new ActionCommand(ShowAboutWindow);
            CloseWindowCommand = new CloseMainWindowCommand();
            HelpTopicsCommand = new HelpTopicsCommand();
            TemplatesViewCommand = new TemplatesViewCommand(container, regionManager);
            DuplicateTemplatesViewCommand = new DuplicateTemplatesViewCommand(container, regionManager);
            CloseTabCommand = new CloseTabCommand(regionManager);
            ShowUpdateCheckWindowCommand = new ActionCommand(DisplayUpdateNotification);
        }

        private void DisplayUpdateNotification()
        {
            var updateCheckView = container.Resolve<IUpdateCheckViewModel>().View;
            updateCheckView.Display();
        }

        private void ShowAboutWindow()
        {
            var view = container.Resolve<IAboutViewModel>().View;

            // TODO : Fix owner
            var owner = container.Resolve<IShellView>() as Window;

            if (owner != null)
                view.Owner = owner;

            view.ShowDialog();
        }

        private void ShowOptionsWindow()
        {
            var window = container.Resolve<IOptionsViewModel>().View;
            var windowResult = window.ShowDialog();

            if (windowResult.HasValue && windowResult.Value)
                window.Model.WriteSetings();
        }
    }

}
