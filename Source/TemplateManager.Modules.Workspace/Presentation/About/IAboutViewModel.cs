using System.Collections.Generic;
using System.Windows.Input;

namespace TemplateManager.Modules.Workspace.Presentation.About
{
    public interface IAboutViewModel
    {
        IAboutView View { get; }
        string Title { get; }
        string ProductAndVersion { get; }
        ICommand CloseWindowCommand { get; }
        string AssemblyCopyright { get; }
        IEnumerable<string> Credits { get; }
    }
}