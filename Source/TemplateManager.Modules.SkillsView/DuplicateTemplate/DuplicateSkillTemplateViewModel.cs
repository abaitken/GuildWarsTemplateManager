using System;
using System.Collections.ObjectModel;
using System.Linq;
using TemplateManager.Common.ViewModel;
using TemplateManager.Infrastructure.Controllers;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    // TODO : Defer loading
    internal class DuplicateSkillTemplateViewModel : ViewModelBase, IDuplicateSkillTemplateViewModel
    {
        private static readonly ViewDetails viewDetails = new ViewDetails("DuplicateTemplates",
                                                                          "Duplicate Templates",
                                                                          new Uri(
                                                                              "pack://application:,,,/TemplateManager.Modules.SkillsView;component/Presentation/Resources/DuplicateTemplates.png", UriKind.Absolute));
        
        private readonly IDuplicateSkillTemplateView view;
        private readonly ISkillTemplateService service;

        public DuplicateSkillTemplateViewModel(IDuplicateSkillTemplateView view, ISkillTemplateService service)
        {
            this.view = view;
            this.service = service;

            view.Model = this;

            //service.TemplatesChanged += ServiceBuildsChanged;
        }

        public bool DeleteTemplate(DeleteTemplateArgs obj)
        {
            var result = obj.DuplicateResult;

            var duplicateTemplate = obj.Template;
            var deleteSuccess = service.Delete(duplicateTemplate.Template);

            if (deleteSuccess && result.Count == 2)
                templates.Remove(result);

            return deleteSuccess;
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

        private ObservableCollection<IDuplicateResult> templates;

        public ObservableCollection<IDuplicateResult> Templates
        {
            get
            {

                if (templates == null)
                {
                    var resultQuery = from item in service.AllTemplates
                                      where item.IsValid
                                      group item by item.SkillKey
                                      into g
                                          where g.Count() > 1
                                          select
                                          new DuplicateResult(this,
                                                              from item in g
                                                              select item) as IDuplicateResult;

                    
                    templates = new ObservableCollection<IDuplicateResult>(resultQuery);
                }

                return templates;
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