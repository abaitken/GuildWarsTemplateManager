using System;
using System.Collections.Generic;
using System.Windows.Input;
using TemplateManager.Infrastructure.Model;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    public interface IDuplicateTemplate
    {
        ICommand DeleteTemplateCommand { get; }
        ISkillTemplate Template { get; }
    }
}