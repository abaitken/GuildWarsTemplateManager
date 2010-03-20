using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using InfiniteRain.Shared.Presentation.Commands;
using InfiniteRain.Shared.Presentation.PresentationModel;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using TemplateManager.Infrastructure;
using TemplateManager.Infrastructure.Controllers;
using TemplateManager.Infrastructure.Services;
using TemplateManager.Modules.Workspace.Presentation.About;
using TemplateManager.Modules.Workspace.Presentation.Options;

namespace TemplateManager.Modules.Workspace.Presentation.Workspace
{
    internal class WorkspaceViewModel : BackgroundLoadingViewModel, IWorkspaceViewModel
    {
        private readonly IApplicationInformationService applicationInformationService;
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;
        private readonly IUpdateService updateService;
        private readonly IWorkspaceView view;
        private readonly IViewManager viewManager;

        public WorkspaceViewModel(IWorkspaceView view,
                                  IUpdateService updateService,
                                  IApplicationInformationService applicationInformationService,
                                  IUnityContainer container,
                                  IRegionManager regionManager,
                                  IViewManager viewManager)
        {
            this.view = view;
            this.updateService = updateService;
            this.applicationInformationService = applicationInformationService;
            this.container = container;
            this.regionManager = regionManager;
            this.viewManager = viewManager;
            view.Model = this;

            GenerateCommands();
        }

        public ICommand OpenRegionView { get; private set; }

        public IEnumerable<MenuItem> ViewItems
        {
            get
            {
                return from item in viewManager.GetViewsForRegion(RegionNames.DocumentRegion)
                       select CreateItem(item);
            }
        }

        #region IWorkspaceViewModel Members

        public IWorkspaceView View
        {
            get { return view; }
        }

        public ICommand ShowOptionsCommand { get; private set; }
        public ICommand AboutCommand { get; private set; }
        public ICommand HelpCommand { get; private set; }
        public ICommand CloseWindowCommand { get; private set; }
        public ICommand HelpTopicsCommand { get; private set; }
        public ICommand CloseTabCommand { get; private set; }
        public ICommand ShowUpdateCheckWindowCommand { get; private set; }

        #endregion

        private void GenerateCommands()
        {
            ShowOptionsCommand = new DelegateCommand<string>(ShowOptionsWindow);
            HelpCommand = new DelegateCommand(OnHelpRequested);
            AboutCommand = new DelegateCommand(ShowAboutWindow);
            CloseWindowCommand = new DelegateCommand<Window>(OnCloseWindow);
            HelpTopicsCommand = new DelegateCommand(OnHelpTopicsRequested);
            CloseTabCommand = new DelegateCommand<TabItem>(OnCloseTab);
            ShowUpdateCheckWindowCommand = new DelegateCommand(DisplayUpdateNotification);
            OpenRegionView = new DelegateCommand<string>(OnOpenRegionView);
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
                                 CommandParameter = details.Key
                             };

            if(details.ImageUri != null)
            {
                var decoder = BitmapDecoder.Create(details.ImageUri,
                                                   BitmapCreateOptions.PreservePixelFormat,
                                                   BitmapCacheOption.Default);

                var source = decoder.Frames[0];
                var image = new Image
                                {
                                    Source = source,
                                    Width = 16,
                                    Height = 16
                                };
                result.Icon = image;
            }
            return result;
        }

        private void OnOpenRegionView(string viewKey)
        {
            viewManager.OpenView(viewKey);
        }

        private void ShowViewRegion<TView, TViewModel>(string regionName, Func<TViewModel, TView> getViewFromViewModel)
        {
            var region = regionManager.Regions[regionName];
            var newView = GetView<TView>(region);

            if(newView != null)
            {
                region.Activate(newView);
            }
            else
            {
                newView = getViewFromViewModel(container.Resolve<TViewModel>());

                region.Add(newView);
                region.Activate(newView);
            }
        }

        private static object GetView<TView>(IRegion region)
        {
            foreach(var view in region.Views)
            {
                if(view is TView)
                    return view;
            }

            return null;
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

        private void ShowOptionsWindow(string regionName)
        {
            ShowViewRegion<IOptionsView, IOptionsViewModel>(regionName, vm => vm.View);
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