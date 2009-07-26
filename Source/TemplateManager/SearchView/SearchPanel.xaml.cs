namespace TemplateManager.SearchView
{
    /// <summary>
    /// Interaction logic for SearchPanel.xaml
    /// </summary>
    public partial class SearchPanel : ISearchView
    {
        public SearchPanel()
        {
            InitializeComponent();
        }

        public ISearchViewModel Model
        {
            get { return DataContext as ISearchViewModel; }
            set { DataContext = value; }
        }
    }
}