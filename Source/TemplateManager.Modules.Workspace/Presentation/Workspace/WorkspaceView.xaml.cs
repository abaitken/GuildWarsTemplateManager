namespace TemplateManager.Modules.Workspace.Presentation.Workspace
{
    /// <summary>
    /// Interaction logic for WorkspaceView.xaml
    /// </summary>
    public partial class WorkspaceView : IWorkspaceView
    {
        public WorkspaceView()
        {
            InitializeComponent();
        }

        #region IWorkspaceView Members

        public IWorkspaceViewModel Model
        {
            get { return DataContext as IWorkspaceViewModel; }
            set { DataContext = value; }
        }

        #endregion
    }
}