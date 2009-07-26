using TemplateManager.Commands;

namespace TemplateManager.Options
{
    abstract class OptionsFolderHelperBase : OpenFolderDialogHelper
    {
        protected readonly IOptionsViewModel viewModel;

        protected OptionsFolderHelperBase(IOptionsViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
    }
}