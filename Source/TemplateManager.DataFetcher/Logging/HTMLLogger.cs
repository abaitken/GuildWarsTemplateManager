using System;
using System.IO;

namespace TemplateManager.DataFetcher.Logging
{
    internal class HTMLLogger : LoggerBase
    {
        private readonly string path;

        private StreamWriter writer;

        public HTMLLogger(ILogFormatter logFormatter, string path)
            : base(logFormatter)
        {
            this.path = path;
        }

        public override void Log(Type sender, string message, LogSeverity logSeverity)
        {
            if(writer == null)
                Initialize();

// ReSharper disable PossibleNullReferenceException
            writer.WriteLine(string.Format(@"<li class=""{0}"">{1}</li>",
                                           logSeverity,
                                           CreateLogEntry(sender, message, logSeverity)));
// ReSharper restore PossibleNullReferenceException
        }

        public override void TearDown()
        {
            if(writer == null)
                return;

            Log(GetType(), "Logging stopped", LogSeverity.InformationLow);
            writer.Write(@"</body>
</html>");
            writer.Flush();
            writer.Close();
        }

        private void Initialize()
        {
            writer = new StreamWriter(path);
            writer.Write(
                @"<html>
    <head>
        <title>Skill Service Log</title>
        <style>
        li
        {
            list-style: none;
        }

        .Unknown
        {
            color: blue;
        }

        .InformationLow
        {
            color: gray;
        }

        .InformationHigh
        {
            color: black;
        }

        .Warning
        {
            color: orange;
        }

        .Error
        {
            color: red;
        }

        .Critical
        {
            color: red;
        }
        </style>
    </head>
    <body>
");
            Log(GetType(), "Logging started", LogSeverity.InformationLow);
        }
    }
}