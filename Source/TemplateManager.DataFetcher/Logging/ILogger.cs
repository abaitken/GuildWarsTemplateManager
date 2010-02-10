using System;

namespace TemplateManager.DataFetcher.Logging
{
    public interface ILogger
    {
        void Log(Type sender, string message, LogSeverity logSeverity);
        void TearDown();
    }
}