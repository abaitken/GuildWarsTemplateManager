using System;

namespace TemplateManager.Infrastructure.Services
{
    public interface IServiceController
    {
        string BuildStore { get; }
        ISkillTemplateService Service { get; }

        event EventHandler TemplatesChanged;
    }
}