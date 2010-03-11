using TemplateManager.Infrastructure.Model;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    public class DeleteTemplateArgs
    {
        private readonly IDuplicateTemplate template;
        private readonly IDuplicateResult duplicateResult;

        public DeleteTemplateArgs(IDuplicateTemplate template, IDuplicateResult duplicateResult)
        {
            this.template = template;
            this.duplicateResult = duplicateResult;
        }

        public IDuplicateResult DuplicateResult
        {
            get { return duplicateResult; }
        }

        public IDuplicateTemplate Template
        {
            get { return template; }
        }
    }
}