namespace TemplateManager.MainView
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : IMainView
    {
        public MainView()
        {
            InitializeComponent();
        }

        #region IMainView Members

        public IMainViewModel Model
        {
            get { return DataContext as IMainViewModel; }
            set { DataContext = value; }
        }

        #endregion
    }
}