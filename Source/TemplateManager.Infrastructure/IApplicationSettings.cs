using System;

namespace TemplateManager.Infrastructure
{
    public interface IApplicationSettings
    {
        string TemplateFolder { get; set; }

        event EventHandler TemplateFolderChanged;

        void Reload();
        void Save();
        void Reset();
    }
}