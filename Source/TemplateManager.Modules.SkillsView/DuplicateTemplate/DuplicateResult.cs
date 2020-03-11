using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TemplateManager.Common;
using TemplateManager.Infrastructure.Model;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    internal class DuplicateResult : ViewModelBase, IDuplicateResult
    {
        private readonly IDuplicateSkillTemplateViewModel parent;
        private readonly ObservableCollection<IDuplicateTemplate> templates;
        private string header;

        public DuplicateResult(IDuplicateSkillTemplateViewModel parent, IEnumerable<SkillTemplate> templates)
        {
            this.parent = parent;
            this.templates = new ObservableCollection<IDuplicateTemplate>(from item in templates
                                                                          select
                                                                              new DuplicateTemplate(this, item) as
                                                                              IDuplicateTemplate);
            UpdateHeader();
            DeleteTemplateCommand = new DelegateCommand<IDuplicateTemplate>(OnDeleteTemplate);
        }

        #region IDuplicateResult Members

        public int Count
        {
            get { return Templates.Count; }
        }

        public ObservableCollection<IDuplicateTemplate> Templates
        {
            get { return templates; }
        }

        public ICommand DeleteTemplateCommand { get; private set; }


        public string Header
        {
            get { return header; }
            set
            {
                if(header == value)
                    return;

                header = value;
                SendPropertyChanged("Header");
            }
        }

        #endregion

        private void UpdateHeader()
        {
            Header = string.Format("{0} duplicate templates", Count);
        }

        private void OnDeleteTemplate(IDuplicateTemplate obj)
        {
            var args = new DeleteTemplateArgs(obj, this);

            if(!parent.DeleteTemplate(args))
                return;

            templates.Remove(obj);
            UpdateHeader();
        }
    }
}