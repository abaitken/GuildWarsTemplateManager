namespace TemplateManager.ShellView
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : IMainWindowView
    {
        public MainWindowView()
        {
            InitializeComponent();
        }

        public IMainWindowViewModel Model
        {
            get { return DataContext as IMainWindowViewModel; }
            set { DataContext = value; }
        }
    }
}