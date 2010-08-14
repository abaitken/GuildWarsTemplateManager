using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using TemperedSoftware.Shared.Presentation;
using TemperedSoftware.Shared.Presentation.Commands;
using TemperedSoftware.Shared.Presentation.PresentationModel;
using TemperedSoftware.Shared.Presentation.ViewManager;
using TemperedSoftware.Shared.Services;
using TemplateManager.Infrastructure;
using TemplateManager.Infrastructure.Controllers;
using TemplateManager.Infrastructure.Services;
using TemplateManager.Modules.Workspace.Presentation.About;

namespace TemplateManager.Modules.Workspace.Presentation.Workspace
{
    internal class WorkspaceViewModel : BackgroundLoadingViewModel, IWorkspaceViewModel
    {
        private readonly IApplicationInformationService applicationInformationService;
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;
        private readonly IUpdateService updateService;
        private readonly IWorkspaceView view;
        private readonly ViewManager viewManager;

        public WorkspaceViewModel(IWorkspaceView view,
                                  IUpdateService updateService,
                                  IApplicationInformationService applicationInformationService,
                                  IUnityContainer container,
                                  IRegionManager regionManager,
                                  ViewManager viewManager)
        {
            this.view = view;
            this.updateService = updateService;
            this.applicationInformationService = applicationInformationService;
            this.container = container;
            this.regionManager = regionManager;
            this.viewManager = viewManager;

            GenerateCommands();
            view.Model = this;
        }

        public ICommand OpenRegionView { get; private set; }

        public IEnumerable<MenuItem> ViewMenuItems
        {
            get { return GetMenuItems(ToolCategories.View); }
        }

        public IEnumerable<MenuItem> ToolMenuItems
        {
            get { return GetMenuItems(ToolCategories.Tool); }
        }

        #region IWorkspaceViewModel Members

        public IWorkspaceView View
        {
            get { return view; }
        }

        public ICommand AboutCommand { get; private set; }
        public ICommand HelpCommand { get; private set; }
        public ICommand CloseWindowCommand { get; private set; }
        public ICommand HelpTopicsCommand { get; private set; }
        public ICommand CloseTabCommand { get; private set; }
        public ICommand ShowUpdateCheckWindowCommand { get; private set; }

        #endregion

        private IEnumerable<MenuItem> GetMenuItems(string category)
        {
            var result = from item in viewManager.GetViewsForCategory(category)
                         select CreateItem(item);

            return result;
        }

        private void GenerateCommands()
        {
            HelpCommand = new DelegateCommand(OnHelpRequested);
            AboutCommand = new DelegateCommand(ShowAboutWindow);
            CloseWindowCommand = new DelegateCommand<Window>(OnCloseWindow);
            HelpTopicsCommand = new DelegateCommand(OnHelpTopicsRequested);
            CloseTabCommand = new DelegateCommand<TabItem>(OnCloseTab);
            ShowUpdateCheckWindowCommand = new DelegateCommand(DisplayUpdateNotification);
            OpenRegionView = new DelegateCommand<ViewDetails>(OnOpenRegionView);
        }

        private void OnCloseTab(TabItem tabItem)
        {
            var region = regionManager.Regions[RegionNames.DocumentRegion];
            region.Remove(tabItem.Content);
        }

        private MenuItem CreateItem(ViewDetails details)
        {
            var result = new MenuItem
                             {
                                 Header = details.Name,
                                 Command = OpenRegionView,
                                 CommandParameter = details
                             };

            if(details.ImageUri != null)
            {
                var image = new Image
                                {
                                    Source = details.ImageUri.CreateSource(),
                                    Width = 16,
                                    Height = 16
                                };
                result.Icon = image;
            }
            return result;
        }

        private void OnOpenRegionView(ViewDetails details)
        {
            if(viewManager.ActivateView(details))
                return;

            viewManager.OpenView(details);
        }


        private static void OnCloseWindow(Window obj)
        {
            obj.Close();
        }

        private static void OnHelpRequested()
        {
            MessageBox.Show("Help is unavailable");
        }

        private static void OnHelpTopicsRequested()
        {
            MessageBox.Show("Help topics are unavailable");
        }

        private void DisplayUpdateNotification()
        {
            var controller = container.Resolve<IUpdateCheckController>();
            controller.ShowDialog();
        }

        private void ShowAboutWindow()
        {
            container.ShowViewDialog<IAboutViewModel>(model => model.View);
        }

        protected override void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            var result = args.Result as IVersionInfo;

            if(result == null)
                return;

            var currentVersion = new Version(applicationInformationService.FileVersion);
            var latestVersion = result.LatestVersion;
            CheckForUpdates(currentVersion, latestVersion);
        }

        private void CheckForUpdates(Version currentVersion, Version latestVersion)
        {
            if(currentVersion >= latestVersion)
                return;

            DisplayUpdateNotification();
        }

        protected override void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = updateService.GetLatestVersionInformation();
        }
    }
}