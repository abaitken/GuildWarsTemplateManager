using System.Collections.Generic;

namespace TemplateManager.Modules.Themes
{
    public interface IThemeManager
    {
        IEnumerable<string> AvailableThemes { get; }
    }
}