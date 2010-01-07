using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;
using TemplateManager.Common.CommandModel;
using TemplateManager.Common.ViewModel;
using TemplateManager.Infrastructure.Events;
using TemplateManager.Infrastructure.Model;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.Modules.SkillsView.SkillView
{
    internal class SkillsViewModel : ViewModelBase, ISkillsViewModel
    {
        private readonly ICommandModel deleteTemplateCommand;
        private readonly ISkillTemplateService service;
        private readonly ISkillsView view;
        private readonly IDataService dataService;
        private SearchParameters searchParameters;
        private SearchParameters searchParametersForSearching;

        public SkillsViewModel(ISkillsView view,
                               IServiceController controller,
                               IDataService dataService)
        {
            this.view = view;
            this.dataService = dataService;
            service = controller.Service;
            deleteTemplateCommand = new DeleteTemplateCommand(controller);
            SearchCommand = new DelegateCommand<object>(OnSearch);
            ResetCommand = new DelegateCommand<object>(OnReset);
            searchParametersForSearching = new SearchParameters();


            controller.TemplatesChanged += ServiceBuildsChanged;

            view.Model = this;
        }

        private void OnReset(object obj)
        {
            SearchParameters = new SearchParameters();
        }

        private void OnSearch(object obj)
        {
            SearchParameters = SearchParametersForSearching;
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
        public SearchParameters SearchParametersForSearching
        {
            get { return searchParametersForSearching; }
            set
            {
                searchParametersForSearching = value;
                SendPropertyChanged("SearchParametersForSearching");
            }
        }

        public IEnumerable<Profession> PrimaryProfessions
        {
            get { return dataService.PrimaryProfessions; }
        }

        public IEnumerable<Profession> SecondaryProfessions
        {
            get { return dataService.SecondaryProfessions; }
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
                var collectionView = CollectionViewSource.GetDefaultView(service.AllTemplates);

                collectionView.Filter = new Predicate<object>(MatchBuild);

                return collectionView;
            }
        }

        public ICommandModel DeleteTemplateCommand
        {
            get { return deleteTemplateCommand; }
        }

        public ICommand SearchCommand { get; private set; }

        public ICommand ResetCommand { get; private set; }

        public string HeaderText
        {
            get { return "Skill Templates"; }
        }

        #endregion

        private void RefreshBuilds()
        {
            SendPropertyChanged("Builds");
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