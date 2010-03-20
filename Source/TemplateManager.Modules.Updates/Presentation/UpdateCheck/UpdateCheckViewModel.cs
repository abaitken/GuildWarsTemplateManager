using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using InfiniteRain.Shared.Presentation.PresentationModel;
using InfiniteRain.Shared.Services;
using Microsoft.Practices.Composite.Presentation.Commands;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.Modules.Updates.Presentation.UpdateCheck
{
    internal class UpdateCheckViewModel : BackgroundLoadingViewModel, IUpdateCheckViewModel
    {
        private readonly IApplicationInformationService informationService;
        private readonly IUpdateService updateService;
        private readonly IUpdateCheckView view;

        public UpdateCheckViewModel(IUpdateCheckView view,
                                    IApplicationInformationService informationService,
                                    IUpdateService updateService)
        {
            this.view = view;
            this.informationService = informationService;
            this.updateService = updateService;
            view.Model = this;

            CreateCommands();
        }

        public ICommand OpenWebAddress { get; private set; }

        #region IUpdateCheckViewModel Members

        public IUpdateCheckView View
        {
            get { return view; }
        }


        string currentVersion;
        public string CurrentVersion
        {
            get { return currentVersion; }
            set
            {
                if (currentVersion == value)
                    return;

                currentVersion = value;
                SendPropertyChanged("CurrentVersion");
            }
        }


        string latestVersion;
        public string LatestVersion
        {
            get { return latestVersion; }
            set
            {
                if (latestVersion == value)
                    return;

                latestVersion = value;
                SendPropertyChanged("LatestVersion");
            }
        }


        string informationUrl;
        public string InformationUrl
        {
            get { return informationUrl; }
            set
            {
                if (informationUrl == value)
                    return;

                informationUrl = value;
                SendPropertyChanged("InformationUrl");
            }
        }

        public ICommand CloseWindowCommand { get; private set; }

        #endregion

        private void CreateCommands()
        {
            CloseWindowCommand = new DelegateCommand<Window>(OnCloseWindow);
            OpenWebAddress = new DelegateCommand<string>(OnOpenWebAddress);
        }

        private static void OnOpenWebAddress(string obj)
        {
            Process.Start(obj);
        }

        private static void OnCloseWindow(Window obj)
        {
            obj.Close();
        }

        protected override void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            CurrentVersion = informationService.FileVersion;

            var result = args.Result as IVersionInfo;

            if(result == null)
                return;

            InformationUrl = result.InformationUrl;
            LatestVersion = result.LatestVersion.ToString();
        }

        protected override void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var result = updateService.GetLatestVersionInformation();

            e.Result = result;
        }
    }
}