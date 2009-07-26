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

        public ISkillsViewModel Model
        {
            get { return DataContext as ISkillsViewModel; }
            set { DataContext = value; }
        }
    }
}