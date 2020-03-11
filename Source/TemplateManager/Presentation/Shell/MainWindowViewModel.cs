using TemplateManager.Common;

namespace TemplateManager.Presentation.Shell
{
    internal class MainWindowViewModel : IMainWindowViewModel
    {
        private readonly IApplicationInformationService service;
        private readonly IMainWindowView view;

        public MainWindowViewModel(IMainWindowView view, IApplicationInformationService service)
        {
            this.view = view;
            this.service = service;
            view.Model = this;
        }

        #region IMainWindowViewModel Members

        public IMainWindowView View
        {
            get { return view; }
        }


        public string WindowTitle
        {
            get { return service.AssemblyProduct; }
        }

        public void ShowView()
        {
            view.Show();
        }

        #endregion
    }
}