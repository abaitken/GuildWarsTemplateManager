using TemperedSoftware.Shared.Presentation;

namespace TemplateManager.Modules.DataExplorer.Presentation.DataExplorer
{
    public interface IDataExplorerView : IHeadedContent
    {
        IDataExplorerViewModel Model { get; set; }
    }
}