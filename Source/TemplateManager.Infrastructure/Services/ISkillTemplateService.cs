using System.Collections.Generic;
using TemplateManager.Infrastructure.Model;

namespace TemplateManager.Infrastructure.Services
{
    public interface ISkillTemplateService
    {
        IList<SkillTemplate> Templates { get; }
        void RefreshTemplates(string buildStore);
    }
}