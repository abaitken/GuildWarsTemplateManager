using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TemplateManager.Infrastructure.Model;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    public interface IDuplicateResult
    {
        ObservableCollection<IDuplicateTemplate> Templates { get; }
        ICommand DeleteTemplateCommand { get; }
        int Count { get; }
    }
}