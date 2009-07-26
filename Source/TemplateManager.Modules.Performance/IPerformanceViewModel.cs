using TemplateManager.Infrastructure.Interfaces;

namespace TemplateManager.Modules.Performance
{
    public interface IPerformanceViewModel : IHeadedContent
    {
        IPerformanceView View { get; }
        string MemoryUsage { get; }
    }
}