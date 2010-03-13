namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    /// <summary>
    /// Interaction logic for DuplicateSkillTemplateView.xaml
    /// </summary>
    public partial class DuplicateSkillTemplateView : IDuplicateSkillTemplateView
    {
        public DuplicateSkillTemplateView()
        {
            InitializeComponent();
            Loaded += DuplicateSkillTemplateView_Loaded;
        }

        void DuplicateSkillTemplateView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.OnViewLoaded();
        }

        #region IDuplicateSkillTemplateView Members

        public IDuplicateSkillTemplateViewModel Model
        {
            get { return DataContext as IDuplicateSkillTemplateViewModel; }
            set { DataContext = value; }
        }

        public string HeaderText
        {
            get { return Model.HeaderText; }
        }

        #endregion
    }
}