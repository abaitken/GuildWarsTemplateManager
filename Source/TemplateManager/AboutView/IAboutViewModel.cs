using System.Collections.Generic;
using System.Windows.Input;
using TemplateManager.Common.CommandModel;

namespace TemplateManager.AboutView
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