using System.Collections.Generic;
using TemplateManager.Infrastructure;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    public interface IDuplicateSkillTemplateViewModel
    {
        IDuplicateSkillTemplateView View { get; }
        IEnumerable<DuplicateTemplate> Templates { get; }
        string HeaderText { get;  }
    }
}