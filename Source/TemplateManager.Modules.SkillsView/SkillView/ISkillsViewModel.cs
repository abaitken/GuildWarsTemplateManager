using System.ComponentModel;
using System.Windows.Input;

namespace TemplateManager.Modules.SkillsView.SkillView
{
    public interface ISkillsViewModel
    {
        ISkillsView View { get; }
        ICollectionView Builds { get; }
        ICommand SearchCommand { get; }
        ICommand ResetCommand { get; }
        string HeaderText { get; }
        void OnViewLoaded();
    }
}