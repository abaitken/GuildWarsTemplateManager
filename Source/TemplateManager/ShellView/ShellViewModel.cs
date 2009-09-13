namespace TemplateManager.ShellView
{
    class ShellViewModel : IShellViewModel
    {
        private readonly IShellView view;

        public ShellViewModel(IShellView view)
        {
            this.view = view;
            view.Model = this;
        }

        public IShellView View
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