using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using TemplateManager.Commands;
using TemplateManager.Common.CommandModel;

namespace TemplateManager.ShellView
{
    internal class ShellViewModel : IShellViewModel
    {
        private readonly IShellView view;
        private readonly IRegionManager regionManager;

        public ShellViewModel(IShellView view, IUnityContainer container, IRegionManager regionManager)
        {
            this.view = view;
            this.regionManager = regionManager;
            view.Model = this;

            GenerateCommands(container);
        }

        #region IShellViewModel Members

        public IShellView View
        {
            get { return view; }
        }

        public string WindowTitle
        {
            get { return "Build Manager"; }
        }

        public void ShowView()
        {
            view.Show();
        }

        public ICommandModel ShowOptionsCommand { get; private set; }
        public ICommandModel AboutCommand { get; private set; }
        public ICommandModel HelpCommand { get; private set; }
        public ICommandModel CloseWindowCommand { get; private set; }
        public ICommandModel HelpTopicsCommand { get; private set; }
        public ICommandModel TemplatesViewCommand { get; private set; }
        public ICommandModel DuplicateTemplatesViewCommand { get; private set; }
        public ICommandModel CloseTabCommand { get; private set; }

        #endregion

        private void GenerateCommands(IUnityContainer container)
        {
            ShowOptionsCommand = new ShowOptionsCommand(container);
            HelpCommand = new HelpTopicsCommand();
            AboutCommand = new AboutCommand(container);
            CloseWindowCommand = new CloseMainWindowCommand();
            HelpTopicsCommand = new HelpTopicsCommand();
            TemplatesViewCommand = new TemplatesViewCommand(container, container.Resolve<IRegionManager>());
            DuplicateTemplatesViewCommand = new DuplicateTemplatesViewCommand(container, container.Resolve<IRegionManager>());
            CloseTabCommand = new CloseTabCommand(regionManager);
        }
    }
}