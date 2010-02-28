using TemplateManager.Infrastructure;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    public interface IDuplicateSkillTemplateView : IHeadedContent
    {
        IDuplicateSkillTemplateViewModel Model { get; set; }
    }
}