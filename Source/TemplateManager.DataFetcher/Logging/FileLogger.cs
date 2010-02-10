using System;
using System.IO;

namespace TemplateManager.DataFetcher.Logging
{
    internal class FileLogger : LoggerBase
    {
        private readonly string path;

        private StreamWriter writer;

        public FileLogger(ILogFormatter logFormatter, string path)
            : base(logFormatter)
        {
            this.path = path;
        }

        public override void Log(Type sender, string message, LogSeverity logSeverity)
        {
            if(writer == null)
                Initialize();

// ReSharper disable PossibleNullReferenceException
            writer.WriteLine(CreateLogEntry(sender, message, logSeverity));
// ReSharper restore PossibleNullReferenceException
        }

        public override void TearDown()
        {
            if(writer == null)
                return;

            Log(GetType(), "Logging stopped", LogSeverity.InformationLow);
            writer.Flush();
            writer.Close();
        }

        private void Initialize()
        {
            writer = new StreamWriter(path, true);
            Log(GetType(), "Logging started", LogSeverity.InformationLow);
        }
    }
}