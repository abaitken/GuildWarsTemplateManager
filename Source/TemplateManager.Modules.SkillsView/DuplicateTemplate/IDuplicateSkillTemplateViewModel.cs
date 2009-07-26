using System.Collections.Generic;
using TemplateManager.Infrastructure.Interfaces;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    public interface IDuplicateSkillTemplateViewModel : IHeadedContent
    {
        IDuplicateSkillTemplateView View { get; }
        IEnumerable<TemplateManager.Modules.SkillsView.DuplicateTemplate.DuplicateTemplate> Templates { get; }
    }
}