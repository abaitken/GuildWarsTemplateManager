using System.Collections.ObjectModel;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    public interface IDuplicateSkillTemplateViewModel
    {
        IDuplicateSkillTemplateView View { get; }
        ObservableCollection<IDuplicateResult> Templates { get; }
        string HeaderText { get; }
        bool DeleteTemplate(DeleteTemplateArgs args);
        void OnViewLoaded();
    }
}