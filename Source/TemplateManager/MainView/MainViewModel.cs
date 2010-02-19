using System;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using TemplateManager.Common.Commands;
using TemplateManager.Infrastructure;
using TemplateManager.Infrastructure.Services;
using TemplateManager.Modules.DataExplorer.Presentation.DataExplorer;
using TemplateManager.Modules.SkillsView.DuplicateTemplate;
using TemplateManager.Modules.SkillsView.SkillView;
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

        #region IMainWindowViewModel Members

        public IMainView View
        {
            get { return view; }
        }

        public ICommand ShowOptionsCommand { get; private set; }
        public ICommand AboutCommand { get; private set; }
        public ICommand HelpCommand { get; private set; }
        public ICommand CloseWindowCommand { get; private set; }
        public ICommand HelpTopicsCommand { get; private set; }
        public ICommand TemplatesViewCommand { get; private set; }
        public ICommand DuplicateTemplatesViewCommand { get; private set; }
        public ICommand CloseTabCommand { get; private set; }
        public ICommand ShowUpdateCheckWindowCommand { get; private set; }
        public ICommand ExploreDataCommand { get; private set; }

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
            ShowOptionsCommand = new DelegateCommand<string>(ShowOptionsWindow);
            HelpCommand = new DelegateCommand(OnHelpRequested);
            AboutCommand = new DelegateCommand(ShowAboutWindow);
            CloseWindowCommand = new DelegateCommand<Window>(OnCloseWindow);
            HelpTopicsCommand = new DelegateCommand(OnHelpTopicsRequested);
            TemplatesViewCommand = new DelegateCommand<string>(OnShowTemplates);
            DuplicateTemplatesViewCommand = new DelegateCommand<string>(OnShowDuplicateTemplates);
            CloseTabCommand = new DelegateCommand<TabItem>(OnCloseTab);
            ShowUpdateCheckWindowCommand = new DelegateCommand(DisplayUpdateNotification);
            ExploreDataCommand = new DelegateCommand<string>(OnExploreData);
        }

        private void OnExploreData(string regionName)
        {
            ShowView<IDataExplorerView, IDataExplorerViewModel>(regionName, vm => vm.View);
        }

        private void OnCloseTab(TabItem tabItem)
        {
            var region = regionManager.Regions[RegionNames.DocumentRegion];
            region.Remove(tabItem.Content);
        }

        private void OnShowDuplicateTemplates(string regionName)
        {
            ShowView<IDuplicateSkillTemplateView, IDuplicateSkillTemplateViewModel>(regionName, vm => vm.View);
        }

        private void OnShowTemplates(string regionName)
        {
            ShowView<ISkillsView, ISkillsViewModel>(regionName, vm => vm.View);
        }

        private void ShowView<TView, TViewModel>(string regionName, Func<TViewModel, TView> getViewFromViewModel)
        {
            var region = regionManager.Regions[regionName];
            var view = GetView<TView>(region);

            if(view != null)
            {
                region.Activate(view);
            }
            else
            {
                view = getViewFromViewModel(container.Resolve<TViewModel>());

                region.Add(view);
                region.Activate(view);
            }
        }

        static object GetView<TView>(IRegion region)
        {
            foreach (var view in region.Views)
            {
                if (view is TView)
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
            ShowView<IUpdateCheckViewModel>(model => model.View);
        }

        private void ShowAboutWindow()
        {
            ShowView<IAboutViewModel>(model => model.View);
        }

        private void ShowView<TModel>(Func<TModel, object> viewSelector)
        {
            var model = container.Resolve<TModel>();
            var viewWindow = viewSelector(model) as Window;

            if(viewWindow == null)
                throw new InvalidOperationException("Expected view to be a window");

            viewWindow.Owner = Application.Current.MainWindow;
            viewWindow.ShowDialog();
        }

        private void ShowOptionsWindow(string regionName)
        {
            ShowView<IOptionsView, IOptionsViewModel>(regionName, vm => vm.View);
        }
    }

}
