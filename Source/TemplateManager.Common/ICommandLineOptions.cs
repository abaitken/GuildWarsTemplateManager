using System.Collections.Generic;

namespace TemplateManager.Common
{
    public interface ICommandLineOptions
    {
        List<string> ParsingErrors { get; set; }
        bool IsValid { get; set; }
    }
}
