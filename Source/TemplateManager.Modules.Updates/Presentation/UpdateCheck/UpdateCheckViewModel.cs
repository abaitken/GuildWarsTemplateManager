using Prism.Commands;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using TemplateManager.Common;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.Modules.Updates.Presentation.UpdateCheck
{
    internal class UpdateCheckViewModel : BackgroundLoadingViewModel, IUpdateCheckViewModel
    {
        private readonly IApplicationInformationService informationService;
        private readonly IUpdateService updateService;
        private readonly IUpdateCheckView view;
        private string currentVersion;
        private string downloadUrl;
        private string informationUrl;
        private string latestVersion;

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

        public string DownloadUrl
        {
            get { return informationUrl; }
            set
            {
                if(downloadUrl == value)
                    return;

                downloadUrl = value;
                SendPropertyChanged("DownloadUrl");
            }
        }

        #region IUpdateCheckViewModel Members

        public IUpdateCheckView View
        {
            get { return view; }
        }


        public string CurrentVersion
        {
            get { return currentVersion; }
            set
            {
                if(currentVersion == value)
                    return;

                currentVersion = value;
                SendPropertyChanged("CurrentVersion");
            }
        }


        public string LatestVersion
        {
            get { return latestVersion; }
            set
            {
                if(latestVersion == value)
                    return;

                latestVersion = value;
                SendPropertyChanged("LatestVersion");
            }
        }


        public string InformationUrl
        {
            get { return informationUrl; }
            set
            {
                if(informationUrl == value)
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
            CurrentVersion = CreateVersionText(informationService.FileVersion, informationService.AssemblyConfiguration);

            var result = args.Result as IVersionInfo;

            if(result == null)
                return;

            InformationUrl = result.InformationUrl;
            LatestVersion = CreateVersionText(result.LatestVersion.ToString(), result.Configuration);
            DownloadUrl = result.DownloadUrl;
        }

        private static string CreateVersionText(string version, string configuration)
        {
            return string.Format("{0} ({1})", version, configuration);
        }

        protected override void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var result = updateService.GetLatestVersionInformation();

            e.Result = result;
        }
    }
}