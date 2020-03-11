using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using TemplateManager.Common;
using TemplateManager.Infrastructure;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.Modules.SkillsView.DuplicateTemplate
{
    internal class DuplicateSkillTemplateViewModel : BackgroundLoadingViewModel, IDuplicateSkillTemplateViewModel
    {
        private static readonly ViewDetails viewDetails = new ViewDetails("Duplicate Templates",
                                                                          ToolCategories.View,
                                                                          new Uri(
                                                                              "pack://application:,,,/TemplateManager.Modules.SkillsView;component/Presentation/Resources/DuplicateTemplates.png",
                                                                              UriKind.Absolute));

        private readonly ISkillTemplateService service;
        private readonly IDuplicateSkillTemplateView view;
        private ObservableCollection<IDuplicateResult> templates;

        public DuplicateSkillTemplateViewModel(IDuplicateSkillTemplateView view, ISkillTemplateService service)
        {
            this.view = view;
            this.service = service;
            templates = new ObservableCollection<IDuplicateResult>();
            view.Model = this;

            //service.TemplatesChanged += ServiceBuildsChanged;
        }

        public static ViewDetails ViewDetails
        {
            get { return viewDetails; }
        }

        #region IDuplicateSkillTemplateViewModel Members

        public bool DeleteTemplate(DeleteTemplateArgs obj)
        {
            var result = obj.DuplicateResult;

            var duplicateTemplate = obj.Template;
            var deleteSuccess = service.Delete(duplicateTemplate.Template);

            if(deleteSuccess && result.Count == 2)
                templates.Remove(result);

            return deleteSuccess;
        }

        public IDuplicateSkillTemplateView View
        {
            get { return view; }
        }

        public ObservableCollection<IDuplicateResult> Templates
        {
            get { return templates; }

            set
            {
                if(templates == value)
                    return;

                templates = value;
                SendPropertyChanged("Templates");
            }
        }

        public string HeaderText
        {
            get { return ViewDetails.Name; }
        }

        #endregion

        protected override void WorkerDoWork(object sender, DoWorkEventArgs e)
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


            var result = new ObservableCollection<IDuplicateResult>(resultQuery);

            e.Result = result;
        }

        protected override void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = e.Result as ObservableCollection<IDuplicateResult>;

            if(result == null)
                return;

            Templates = result;
        }

        private void ServiceBuildsChanged(object sender, EventArgs e)
        {
        }
    }
}