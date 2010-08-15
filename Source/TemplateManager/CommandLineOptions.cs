using TemperedSoftware.Shared.CommandLine;

namespace TemplateManager
{
    internal class CommandLineOptions : CommandLineOptionsBase
    {
        [HelpText("Reset")]
        [DefaultValue(false)]
        [Option("reset"), Option("r")]
        public bool Reset { get; set; }
    }
}