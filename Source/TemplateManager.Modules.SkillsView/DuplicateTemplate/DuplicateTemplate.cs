using System.Windows.Input;
using TemplateManager.Infrastructure.Model;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    internal class DuplicateTemplate : IDuplicateTemplate
    {
        private readonly ISkillTemplate inner;
        private readonly IDuplicateResult parent;

        public DuplicateTemplate(IDuplicateResult parent, ISkillTemplate inner)
        {
            this.parent = parent;
            this.inner = inner;
        }

        #region IDuplicateTemplate Members

        public ICommand DeleteTemplateCommand
        {
            get { return parent.DeleteTemplateCommand; }
        }

        public ISkillTemplate Template
        {
            get { return inner; }
        }

        #endregion
    }
}