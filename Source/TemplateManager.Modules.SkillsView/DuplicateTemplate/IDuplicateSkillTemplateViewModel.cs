using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TemplateManager.Infrastructure;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    public interface IDuplicateSkillTemplateViewModel
    {
        IDuplicateSkillTemplateView View { get; }
        ObservableCollection<IDuplicateResult> Templates { get; }
        string HeaderText { get;  }
        bool DeleteTemplate(DeleteTemplateArgs args);
    }
}