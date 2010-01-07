using System.ComponentModel;
using System.Windows.Input;
using TemplateManager.Infrastructure.Interfaces;

namespace TemplateManager.Modules.SkillsView.SkillView
{
    public interface ISkillsViewModel : IHeadedContent
    {
        ISkillsView View { get; }
        ICollectionView Builds { get; }
        ICommand DeleteTemplateCommand { get; }
        ICommand SearchCommand { get; }
        ICommand ResetCommand { get; }
    }
}