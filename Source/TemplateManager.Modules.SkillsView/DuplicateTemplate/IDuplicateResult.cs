using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    public interface IDuplicateResult
    {
        ObservableCollection<IDuplicateTemplate> Templates { get; }
        ICommand DeleteTemplateCommand { get; }
        string Header { get; }
        int Count { get; }
    }
}