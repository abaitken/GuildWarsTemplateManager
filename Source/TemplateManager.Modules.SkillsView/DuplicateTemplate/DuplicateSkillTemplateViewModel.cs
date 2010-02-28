using System;
using System.Collections.Generic;
using System.Linq;
using TemplateManager.Common.ViewModel;
using TemplateManager.Infrastructure.Controllers;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    internal class DuplicateSkillTemplateViewModel : ViewModelBase, IDuplicateSkillTemplateViewModel
    {
        private static readonly ViewDetails viewDetails = new ViewDetails("DuplicateTemplates",
                                                                          "Duplicate Templates",
                                                                          new Uri(
                                                                              "pack://application:,,,/TemplateManager.Modules.SkillsView;component/Presentation/Resources/DuplicateTemplates.png", UriKind.Absolute));
        private readonly ISkillTemplateService service;
        private readonly IDuplicateSkillTemplateView view;

        public DuplicateSkillTemplateViewModel(IDuplicateSkillTemplateView view, IServiceController controller)
        {
            this.view = view;
            service = controller.Service;
            view.Model = this;

            controller.TemplatesChanged += ServiceBuildsChanged;
        }

        public static ViewDetails ViewDetails
        {
            get { return viewDetails; }
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
                return from item in service.AllTemplates
                       where item.IsValid
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
            get { return ViewDetails.Name; }
        }

        #endregion

        private void ServiceBuildsChanged(object sender, EventArgs e)
        {
            SendPropertyChanged("Templates");
        }
    }
}