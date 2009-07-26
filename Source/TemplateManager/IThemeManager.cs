using System.Collections.Generic;

namespace TemplateManager
{
    public interface IThemeManager
    {
        IEnumerable<string> AvailableThemes { get; }
    }
}