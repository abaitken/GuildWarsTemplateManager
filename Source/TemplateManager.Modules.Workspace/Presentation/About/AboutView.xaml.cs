namespace TemplateManager.Modules.Workspace.Presentation.About
{
    /// <summary>
    /// Interaction logic for AboutDialog.xaml
    /// </summary>
    public partial class AboutView : IAboutView
    {
        public AboutView()
        {
            InitializeComponent();
        }

        #region IAboutView Members

        public IAboutViewModel Model
        {
            get { return DataContext as IAboutViewModel; }
            set { DataContext = value; }
        }

        #endregion
    }
}