namespace TemplateManager.Options
{
    /// <summary>
    /// Interaction logic for BuildStoreSelector.xaml
    /// </summary>
    public partial class OptionsWindow : IOptionsView
    {
        public OptionsWindow()
        {
            InitializeComponent();
        }

        #region IOptionsView Members

        public IOptionsViewModel Model
        {
            get { return DataContext as IOptionsViewModel; }
            set { DataContext = value; }
        }

        #endregion
    }
}