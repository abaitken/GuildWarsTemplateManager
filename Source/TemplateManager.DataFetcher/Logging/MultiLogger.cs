using System;
using System.Collections.Generic;

namespace TemplateManager.DataFetcher.Logging
{
    internal class MultiLogger : ILogger
    {
        private readonly IEnumerable<ILogger> loggers;

        public MultiLogger(IEnumerable<ILogger> loggers)
        {
            this.loggers = loggers;
        }

        #region ILogger Members

        public void Log(Type sender, string message, LogSeverity logSeverity)
        {
            foreach(var logger in loggers)
                logger.Log(sender, message, logSeverity);
        }

        public void TearDown()
        {
            foreach(var logger in loggers)
                logger.TearDown();
        }

        #endregion
    }
}