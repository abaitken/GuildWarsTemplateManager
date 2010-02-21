using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.UpdateCheck
{
    internal class UpdateCheckViewModel : IUpdateCheckViewModel
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

        public string CurrentVersion
        {
            get { return informationService.FileVersion; }
        }

        public string LatestVersion
        {
            get { return updateService.LatestVersion.ToString(); }
        }

        public string InformationUrl
        {
            get { return updateService.InformationUrl; }
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
    }
}