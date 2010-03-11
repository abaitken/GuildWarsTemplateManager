namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    public class DeleteTemplateArgs
    {
        private readonly IDuplicateResult duplicateResult;
        private readonly IDuplicateTemplate template;

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