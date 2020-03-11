using System.ComponentModel;

namespace TemplateManager.Common
{
    public abstract class CommandLineOptionsWithHelp : CommandLineOptionsBase
    {
        [HelpText("Help", "Display command line help")]
        [DefaultValue(false)]
        [Option("h"), Option("help"), Option("?")]
        public bool HelpRequested { get; set; }
    }
}
