using System.Windows;

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

        #region IDataExplorerView Members

        public IDataExplorerViewModel Model
        {
            get { return DataContext as IDataExplorerViewModel; }
            set { DataContext = value; }
        }

        public string HeaderText
        {
            get { return Model.HeaderText; }
        }

        #endregion

        private void DataExplorerView_Loaded(object sender, RoutedEventArgs e)
        {
            Model.ViewLoaded();
        }
    }
}