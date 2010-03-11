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