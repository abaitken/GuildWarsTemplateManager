using System.Collections.Generic;
using System.Linq;
using TemplateManager.Infrastructure.Model;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    public struct DuplicateTemplate
    {
        private readonly IEnumerable<SkillTemplate> templates;

        public DuplicateTemplate(IEnumerable<SkillTemplate> templates)
            : this()
        {
            this.templates = templates;
        }


        public IEnumerable<SkillTemplate> Templates
        {
            get { return templates; }
        }

        public override string ToString()
        {
            return string.Format("{0} duplicate templates", Templates.Count());
        }
    }
}