using System;

namespace TemplateManager.DataFetcher.Logging
{
    internal abstract class LoggerBase : ILogger
    {
        private readonly ILogFormatter logFormatter;

        protected LoggerBase(ILogFormatter logFormatter)
        {
            this.logFormatter = logFormatter;
        }

        #region ILogger Members

        public abstract void Log(Type sender, string message, LogSeverity logSeverity);
        public abstract void TearDown();

        #endregion

        protected string CreateLogEntry(Type sender, string message, LogSeverity logSeverity)
        {
            return logFormatter.Format(sender.Name, message, logSeverity);
        }
    }
}