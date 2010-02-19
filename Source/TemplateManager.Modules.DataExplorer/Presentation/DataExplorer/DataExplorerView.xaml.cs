namespace TemplateManager.Modules.DataExplorer.Presentation.DataExplorer
{
    /// <summary>
    /// Interaction logic for DataExplorerView.xaml
    /// </summary>
    public partial class DataExplorerView : IDataExplorerView
    {
        public DataExplorerView()
        {
            InitializeComponent();
            Loaded += DataExplorerView_Loaded;
        }

        void DataExplorerView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.ViewLoaded();
        }

        public IDataExplorerViewModel Model
        {
            get { return DataContext as IDataExplorerViewModel; }
            set { DataContext = value; }
        }
    }
}
