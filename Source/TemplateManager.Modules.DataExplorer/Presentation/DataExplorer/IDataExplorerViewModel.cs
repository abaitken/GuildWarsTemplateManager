using TemplateManager.Infrastructure.Interfaces;

namespace TemplateManager.Modules.DataExplorer.Presentation.DataExplorer
{
    public interface IDataExplorerViewModel : IHeadedContent
    {
        IDataExplorerView View { get; }
        void ViewLoaded();
    }
}