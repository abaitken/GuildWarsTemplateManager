using System;

namespace TemplateManager.Infrastructure
{
    public interface IApplicationSettings
    {
        string TemplateFolder { get; set; }
        string ArchiveFolder { get; set; }
        string DeleteBehaviour { get; set; }

        event EventHandler TemplateFolderChanged;
        event EventHandler ArchiveFolderChanged;
        event EventHandler DeleteBehaviourChanged;

        void Reload();
        void Save();
        void Reset();
    }
}