using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using TemplateManager.Common.ViewModel;
using TemplateManager.Infrastructure.Model;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    internal class DuplicateResult : ViewModelBase, IDuplicateResult
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
            UpdateHeader();
            DeleteTemplateCommand = new DelegateCommand<IDuplicateTemplate>(OnDeleteTemplate);
        }

        private void UpdateHeader()
        {
            Header = string.Format("{0} duplicate templates", Count);
        }
        
        public int Count
        {
            get { return Templates.Count; }
        }

        #region IDuplicateResult Members

        public ObservableCollection<IDuplicateTemplate> Templates
        {
            get { return templates; }
        }

        public ICommand DeleteTemplateCommand { get; private set; }


        string header;
        public string Header
        {
            get { return header; }
            set
            {
                if (header == value)
                    return;

                header = value;
                SendPropertyChanged("Header");
            }
        }


        #endregion

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