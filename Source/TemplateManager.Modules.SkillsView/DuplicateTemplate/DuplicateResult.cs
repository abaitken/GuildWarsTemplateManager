using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using TemplateManager.Infrastructure.Model;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    // TODO : Post updates to count, bind in UI
    internal class DuplicateResult : IDuplicateResult
    {
        private readonly IDuplicateSkillTemplateViewModel parent;
        private readonly ObservableCollection<IDuplicateTemplate> templates;

        public DuplicateResult(IDuplicateSkillTemplateViewModel parent, IEnumerable<SkillTemplate> templates)
        {
            this.parent = parent;
            this.templates = new ObservableCollection<IDuplicateTemplate>(from item in templates
                                                                          select
                                                                              new DuplicateTemplate(this, item) as
                                                                              IDuplicateTemplate);

            DeleteTemplateCommand = new DelegateCommand<IDuplicateTemplate>(OnDeleteTemplate);
        }

        #region IDuplicateResult Members

        public ObservableCollection<IDuplicateTemplate> Templates
        {
            get { return templates; }
        }

        public ICommand DeleteTemplateCommand { get; private set; }

        public int Count
        {
            get { return Templates.Count; }
        }

        #endregion

        private void OnDeleteTemplate(IDuplicateTemplate obj)
        {
            var args = new DeleteTemplateArgs(obj, this);
            if(parent.DeleteTemplate(args))
                templates.Remove(obj);
        }

        public override string ToString()
        {
            return string.Format("{0} duplicate templates", Count);
        }
    }
}