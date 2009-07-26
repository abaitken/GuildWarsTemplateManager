using System.ComponentModel;
using TemplateManager.Common.CommandModel;
using TemplateManager.Infrastructure.Interfaces;

namespace TemplateManager.Modules.SkillsView.SkillView
{
    public interface ISkillsViewModel : IHeadedContent
    {
        ISkillsView View { get; }
        ICollectionView Builds { get; }
        ICommandModel DeleteTemplateCommand { get; }
    }
}