using System.Collections.Generic;
using System.Globalization;
using TemplateManager.Commands;
using TemplateManager.Common.CommandModel;
using TemplateManager.Common.ViewModel;

namespace TemplateManager.AboutView
{
    internal class AboutViewModel : ViewModelBase, IAboutViewModel
    {
        private readonly IAboutView view;
        private readonly ApplicationInformation applicationInformation;

        public AboutViewModel(IAboutView view)
        {
            this.view = view;
            view.Model = this;

            applicationInformation = new ApplicationInformation();
            CloseWindowCommand = new GenericCloseWindowCommand();
        }
        
        public string AssemblyCopyright
        {
            get
            {
                return applicationInformation.AssemblyCopyright;
            }
        }

        public string Title
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture, "About {0}", applicationInformation.AssemblyTitle);
            }
        }

        public string ProductAndVersion
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture, "{0} v{1} {2}", 
                                     applicationInformation.AssemblyProduct,  
                                     applicationInformation.FileVersion, 
                                     applicationInformation.AssemblyConfiguration);
            }
        }

        public ICommandModel CloseWindowCommand { get; private set; }

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