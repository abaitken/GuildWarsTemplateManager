namespace TemplateManager.ShellView
{
    class MainWindowViewModel : IMainWindowViewModel
    {
        private readonly IMainWindowView view;

        public MainWindowViewModel(IMainWindowView view)
        {
            this.view = view;
            view.Model = this;
        }

        public IMainWindowView View
        {
            get { return view; }
        }


        public string WindowTitle
        {
            get { return "Build Manager"; }
        }

        public void ShowView()
        {
            view.Show();
        }
    }

}