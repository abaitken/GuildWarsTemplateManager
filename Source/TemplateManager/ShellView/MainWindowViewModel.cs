namespace TemplateManager.ShellView
{
    class MainWindowViewModel : IMainWindowViewModel
    {
        private readonly IMainWindowView view;
        private readonly IApplicationInformationService service;

        public MainWindowViewModel(IMainWindowView view, IApplicationInformationService service)
        {
            this.view = view;
            this.service = service;
            view.Model = this;
        }

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
    }

}