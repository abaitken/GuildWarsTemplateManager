using TemperedSoftware.Shared.CommandLine;

namespace TemplateManager
{
    internal class CommandLineOptions : CommandLineOptionsBase
    {
        [CommandLineOption("Reset", "", new[] { "reset", "r" }, false)]
        public bool Reset { get; set; }
    }
}