namespace TemplateManager.UpdateCheck
{
    /// <summary>
    /// Interaction logic for UpdateCheck.xaml
    /// </summary>
    public partial class UpdateCheckView : IUpdateCheckView
    {
        public UpdateCheckView()
        {
            InitializeComponent();
        }

        #region IUpdateCheckView Members

        public IUpdateCheckViewModel Model
        {
            get { return DataContext as IUpdateCheckViewModel; }
            set { DataContext = value; }
        }

        #endregion
    }
}