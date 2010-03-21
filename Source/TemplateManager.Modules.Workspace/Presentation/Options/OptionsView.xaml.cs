namespace TemplateManager.Modules.Workspace.Presentation.Options
{
    /// <summary>
    /// Interaction logic for BuildStoreSelector.xaml
    /// </summary>
    public partial class OptionsView : IOptionsView
    {
        public OptionsView()
        {
            InitializeComponent();
        }

        #region IOptionsView Members

        public IOptionsViewModel Model
        {
            get { return DataContext as IOptionsViewModel; }
            set { DataContext = value; }
        }

        public string HeaderText
        {
            get { return Model.HeaderText; }
        }

        #endregion
    }
}