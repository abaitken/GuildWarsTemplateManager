using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using TemplateManager.Commands;
using TemplateManager.Common.CommandModel;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.UpdateCheck
{
    class UpdateCheckViewModel : IUpdateCheckViewModel
    {
        private readonly IUpdateCheckView view;
        private readonly IApplicationInformationService informationService;
        private readonly IUpdateService updateService;

        public UpdateCheckViewModel(IUpdateCheckView view, IApplicationInformationService informationService, IUpdateService updateService)
        {
            this.view = view;
            this.informationService = informationService;
            this.updateService = updateService;
            view.Model = this;

            CreateCommands();
        }

        private void CreateCommands()
        {
            CloseWindowCommand = new DelegateCommand<Window>(OnCloseWindow);
        }

        private void OnCloseWindow(Window obj)
        {
            obj.Close();
        }

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

        public ICommand CloseWindowCommand
        {
            get; private set;
        }
    }

}
