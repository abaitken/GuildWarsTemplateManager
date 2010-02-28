using System;
using System.Windows;
using TemplateManager.Infrastructure;

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

        #endregion

        private void DataExplorerView_Loaded(object sender, RoutedEventArgs e)
        {
            Model.ViewLoaded();
        }

        public string HeaderText
        {
            get { return Model.HeaderText; }
        }
    }
}