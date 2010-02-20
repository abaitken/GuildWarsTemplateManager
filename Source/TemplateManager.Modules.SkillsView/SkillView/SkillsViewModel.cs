using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using TemplateManager.Common.Commands;
using TemplateManager.Common.ViewModel;
using TemplateManager.Infrastructure.Events;
using TemplateManager.Infrastructure.Model;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.Modules.SkillsView.SkillView
{
    internal class SkillsViewModel : ViewModelBase, ISkillsViewModel
    {
        private readonly IDataService dataService;
        private readonly ISkillTemplateService service;
        private readonly ISkillsView view;
        private SearchParameters searchParameters = new SearchParameters();

        public SkillsViewModel(ISkillsView view,
                               IServiceController controller,
                               IDataService dataService)
        {
            this.view = view;
            this.dataService = dataService;
            service = controller.Service;
            GenerateCommands();


            controller.TemplatesChanged += ServiceBuildsChanged;

            view.Model = this;
        }

        public SearchParameters SearchParameters
        {
            get { return searchParameters; }
            set
            {
                if(searchParameters == value)
                    return;

                searchParameters = value;
                SendPropertyChanged("SearchParameters");
            }
        }

        public IEnumerable<IProfession> PrimaryProfessions
        {
            get { return dataService.PrimaryProfessions; }
        }

        public IEnumerable<IProfession> SecondaryProfessions
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

        public ICommand SearchCommand { get; private set; }

        public ICommand ResetCommand { get; private set; }

        public string HeaderText
        {
            get { return "Skill Templates"; }
        }

        #endregion

        private void GenerateCommands()
        {
            SearchCommand = new DelegateCommand(OnSearch);
            ResetCommand = new DelegateCommand(OnReset);
        }

        private void OnReset()
        {
            SearchParameters = new SearchParameters();
            RefreshBuilds();
        }

        private void OnSearch()
        {
            RefreshBuilds();
        }

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