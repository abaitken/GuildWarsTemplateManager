using System;
using System.Collections.Generic;
using System.Linq;
using TemplateManager.Common.ViewModel;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    internal class DuplicateSkillTemplateViewModel : ViewModelBase, IDuplicateSkillTemplateViewModel
    {
        private readonly ISkillTemplateService service;
        private readonly IDuplicateSkillTemplateView view;

        public DuplicateSkillTemplateViewModel(IDuplicateSkillTemplateView view, IServiceController controller)
        {
            this.view = view;
            service = controller.Service;
            view.Model = this;

            controller.TemplatesChanged += ServiceBuildsChanged;
        }

        #region IDuplicateSkillTemplateViewModel Members

        public IDuplicateSkillTemplateView View
        {
            get { return view; }
        }

        public IEnumerable<DuplicateTemplate> Templates
        {
            get
            {
                return from item in service.Templates
                       where !item.IsInvalid
                       group item by item.SkillKey
                       into g
                           where g.Count() > 1
                           select
                           new DuplicateTemplate(from item in g
                                                 select item);
            }
        }

        public string HeaderText
        {
            get { return "Duplicate Skill Templates"; }
        }

        #endregion

        private void ServiceBuildsChanged(object sender, EventArgs e)
        {
            SendPropertyChanged("Templates");
        }
    }
}