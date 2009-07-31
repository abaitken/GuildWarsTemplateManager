using System.Collections.Generic;
using TemplateManager.Infrastructure.Model;

namespace TemplateManager.Infrastructure.Services
{
    public interface ISkillTemplateService
    {
        TemplateFolder TemplateFolder { get; }
        IEnumerable<SkillTemplate> AllTemplates { get; }
        void RefreshTemplates(string buildStore);
    }
}