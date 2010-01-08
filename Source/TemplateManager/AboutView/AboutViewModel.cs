using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using TemplateManager.Common.ViewModel;

namespace TemplateManager.AboutView
{
    internal class AboutViewModel : ViewModelBase, IAboutViewModel
    {
        private readonly IAboutView view;
        private readonly IApplicationInformationService applicationInformationService;

        public AboutViewModel(IAboutView view, IApplicationInformationService applicationInformationService)
        {
            this.view = view;
            this.applicationInformationService = applicationInformationService;
            view.Model = this;

            CloseWindowCommand = new DelegateCommand<Window>(OnCloseWindow);
        }

        private static void OnCloseWindow(Window obj)
        {
            obj.Close();
        }

        public string AssemblyCopyright
        {
            get
            {
                return applicationInformationService.AssemblyCopyright;
            }
        }

        public string Title
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture, "About {0}", applicationInformationService.AssemblyTitle);
            }
        }

        public string ProductAndVersion
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture, "{0} v{1} {2}", 
                                     applicationInformationService.AssemblyProduct,  
                                     applicationInformationService.FileVersion, 
                                     applicationInformationService.AssemblyConfiguration);
            }
        }

        public ICommand CloseWindowCommand { get; private set; }

        public IAboutView View
        {
            get { return view; }
        }

        public IEnumerable<string> Credits
        {
            get 
            {
                yield return "Alex Boyne-Aitken, Lead Programmer";
                yield return "Matt (RavenIII), Artwork";
            }
        }
    }
}