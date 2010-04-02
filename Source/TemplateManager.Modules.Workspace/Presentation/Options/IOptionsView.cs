using TemperedSoftware.Shared.Presentation;

namespace TemplateManager.Modules.Workspace.Presentation.Options
{
    public interface IOptionsView : IHeadedContent
    {
        IOptionsViewModel Model { get; set; }
    }
}