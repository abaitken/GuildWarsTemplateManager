using System;

namespace TemplateManager.DataFetcher.Logging
{
    internal class TimestampFormatter : ILogFormatter
    {
        #region ILogFormatter Members

        public string Format(string sender, string message, LogSeverity severity)
        {
            return string.Format("[{0}] {1}: {2}", DateTime.Now, sender, message);
        }

        #endregion
    }
}