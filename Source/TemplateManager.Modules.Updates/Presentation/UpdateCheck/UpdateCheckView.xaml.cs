using System.Windows;

namespace TemplateManager.Modules.Updates.Presentation.UpdateCheck
{
    /// <summary>
    /// Interaction logic for UpdateCheck.xaml
    /// </summary>
    public partial class UpdateCheckView : IUpdateCheckView
    {
        public UpdateCheckView()
        {
            InitializeComponent();
            Loaded += UpdateCheckView_Loaded;
        }

        #region IUpdateCheckView Members

        public IUpdateCheckViewModel Model
        {
            get { return DataContext as IUpdateCheckViewModel; }
            set { DataContext = value; }
        }

        #endregion

        private void UpdateCheckView_Loaded(object sender, RoutedEventArgs e)
        {
            Model.OnViewLoaded();
        }
    }
}