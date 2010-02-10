namespace TemplateManager.DataFetcher.Logging
{
    public enum LogSensitivity
    {
        All = -1,
        InformationLow = LogSeverity.InformationLow,
        InformationHigh = LogSeverity.InformationHigh,
        Warning = LogSeverity.Warning,
        Error = LogSeverity.Error,
        Critical = LogSeverity.Critical
    }
}