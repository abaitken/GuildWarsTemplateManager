using System.Collections.Generic;

namespace TemplateManager.Common
{
    public abstract class CommandLineOptionsBase : ICommandLineOptions
    {
        public List<string> ParsingErrors { get; set; }
        public bool IsValid { get; set; }
    }
}
