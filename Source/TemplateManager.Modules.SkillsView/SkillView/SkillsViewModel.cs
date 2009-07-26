using System;
using System.ComponentModel;
using System.Windows.Data;
using Microsoft.Practices.Composite.Events;
using TemplateManager.Common.CommandModel;
using TemplateManager.Common.ViewModel;
using TemplateManager.Infrastructure.Events;
using TemplateManager.Infrastructure.Model;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.Modules.SkillsView.SkillView
{
    internal class SkillsViewModel : ViewModelBase, ISkillsViewModel
    {
        private readonly ISkillTemplateService service;
        private readonly ISkillsView view;
        private SearchParameters searchParameters;

        public SkillsViewModel(ISkillsView view,
                               IServiceController controller,
                               IEventAggregator eventAggregator)
        {
            this.view = view;
            service = controller.Service;
            deleteTemplateCommand = new DeleteTemplateCommand(controller);

            eventAggregator.GetEvent<SkillTemplateFilterEvent>().Subscribe(SkillTemplateFilterChanged);

            controller.TemplatesChanged += ServiceBuildsChanged;

            view.Model = this;
        }

        private SearchParameters SearchParameters
        {
            get { return searchParameters; }
            set
            {
                searchParameters = value;
                RefreshBuilds();
            }
        }

        #region ISkillsViewModel Members

        public ISkillsView View
        {
            get { return view; }
        }

        public ICollectionView Builds
        {
            get
            {
                var collectionView = CollectionViewSource.GetDefaultView(service.Templates);

                collectionView.Filter = new Predicate<object>(MatchBuild);

                return collectionView;
            }
        }

        private readonly ICommandModel deleteTemplateCommand;

        public ICommandModel DeleteTemplateCommand
        {
            get { return deleteTemplateCommand; }
        }

        public string HeaderText
        {
            get { return "Skill Templates"; }
        }

        #endregion

        private void RefreshBuilds()
        {
            SendPropertyChanged("Builds");
        }

        private void SkillTemplateFilterChanged(SearchParameters newFilter)
        {
            SearchParameters = newFilter;
        }

        private void ServiceBuildsChanged(object sender, EventArgs e)
        {
            SendPropertyChanged("Builds");
        }

        private bool MatchBuild(object buildObject)
        {
            var build = buildObject as SkillTemplate;

            if(build == null)
                return false;

            if(SearchParameters == null)
                return true;

            return
                (string.IsNullOrEmpty(SearchParameters.Name) ||
                 build.Name.ToLower().Contains(SearchParameters.Name.ToLower())) &&
                (string.IsNullOrEmpty(SearchParameters.Author) ||
                 build.Author.ToLower().Contains(SearchParameters.Author.ToLower())) &&
                (string.IsNullOrEmpty(SearchParameters.Tags) ||
                 build.Tags.ToLower().Contains(SearchParameters.Tags.ToLower())) &&
                (string.IsNullOrEmpty(SearchParameters.Notes) ||
                 build.Notes.ToLower().Contains(SearchParameters.Notes.ToLower())) &&
                ((SearchParameters.ShowInvalidBuilds && build.IsInvalid) ||
                 (SearchParameters.ShowValidBuilds && !build.IsInvalid)) &&
                (SearchParameters.AnyPrimaryProfession ||
                 SearchParameters.PrimaryProfessions.Contains(build.PrimaryProfession)) &&
                (SearchParameters.AnySecondaryProfession ||
                 SearchParameters.SecondaryProfessions.Contains(build.SecondaryProfession));
        }
    }
}