using System;

namespace TemplateManager.DataFetcher.Logging
{
    internal class ConsoleLogger : LoggerBase
    {
        private readonly LogSensitivity sensitivity;

        public ConsoleLogger(ILogFormatter logFormatter, LogSensitivity sensitivity)
            : base(logFormatter)
        {
            this.sensitivity = sensitivity;
        }

        public override void Log(Type sender, string message, LogSeverity logSeverity)
        {
            if(!ShouldLog(sensitivity, logSeverity))
                return;

            Console.WriteLine(CreateLogEntry(sender, message, logSeverity));
        }

        public override void TearDown()
        {
            // Do nothing
        }

        private static bool ShouldLog(LogSensitivity logSensitivity, LogSeverity severity)
        {
            if(logSensitivity == LogSensitivity.All)
                return true;

            var sensitivityValue = (int) logSensitivity;
            var severityValue = (int) severity;

            return severityValue >= sensitivityValue;
        }
    }
}