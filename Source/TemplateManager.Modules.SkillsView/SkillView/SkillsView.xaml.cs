namespace TemplateManager.Modules.SkillsView.SkillView
{
    /// <summary>
    /// Interaction logic for SkillsView.xaml
    /// </summary>
    public partial class SkillsView : ISkillsView
    {
        public SkillsView()
        {
            InitializeComponent();
        }

        #region ISkillsView Members

        public ISkillsViewModel Model
        {
            get { return DataContext as ISkillsViewModel; }
            set { DataContext = value; }
        }

        public string HeaderText
        {
            get { return Model.HeaderText; }
        }

        #endregion
    }
}