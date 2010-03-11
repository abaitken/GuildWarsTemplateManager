using System.Windows.Input;
using TemplateManager.Infrastructure.Model;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    class DuplicateTemplate : IDuplicateTemplate
    {
        private readonly IDuplicateResult parent;
        private readonly ISkillTemplate inner;

        public DuplicateTemplate(IDuplicateResult parent, ISkillTemplate inner)
        {
            this.parent = parent;
            this.inner = inner;
        }

        public ICommand DeleteTemplateCommand
        {
            get { return parent.DeleteTemplateCommand; }
        }

        public ISkillTemplate Template
        {
            get { return inner; }
        }
    }
}