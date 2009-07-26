namespace TemplateManager.AboutView
{
    /// <summary>
    /// Interaction logic for AboutDialog.xaml
    /// </summary>
    public partial class AboutDialog : IAboutView
    {
        public AboutDialog()
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