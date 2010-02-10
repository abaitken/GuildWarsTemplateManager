namespace TemplateManager.DataFetcher.Logging
{
    public interface ILogFormatter
    {
        string Format(string sender, string message, LogSeverity severity);
    }
}