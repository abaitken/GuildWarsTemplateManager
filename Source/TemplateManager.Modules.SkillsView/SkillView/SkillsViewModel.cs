using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using InfiniteRain.Shared.Presentation.Commands;
using InfiniteRain.Shared.Presentation.PresentationModel;
using InfiniteRain.Shared.Presentation.ViewManager;
using TemplateManager.Infrastructure.Controllers;
using TemplateManager.Infrastructure.Model;
using TemplateManager.Infrastructure.Services;
using TemplateManager.Infrastructure;

namespace TemplateManager.Modules.SkillsView.SkillView
{
    internal class SkillsViewModel : BackgroundLoadingViewModel, ISkillsViewModel
    {
        private static readonly ViewDetails viewDetails = new ViewDetails("Skill Templates", ToolCategories.View);
        private readonly IDataService dataService;
        private readonly ISkillTemplateService service;
        private readonly ISkillsView view;
        private ICollectionView collectionView;
        private bool searchAnyPrimaryProfession;
        private bool searchAnySecondaryProfession;
        private string searchAuthor;
        private string searchName;
        private string searchNotes;
        private string searchTags;
        private IList<IProfession> selectedPrimaryProfessions;
        private IList<IProfession> selectedSecondaryProfessions;
        private bool? showValidBuilds;

        public SkillsViewModel(ISkillsView view,
                               ISkillTemplateService service,
                               IDataService dataService)
        {
            this.view = view;
            this.service = service;
            this.dataService = dataService;

            GenerateCommands();
            ResetFilters();

            service.TemplatesChanged += ServiceBuildsChanged;

            view.Model = this;
        }

        public IEnumerable<IProfession> PrimaryProfessions
        {
            get { return dataService.PrimaryProfessions; }
        }

        public IEnumerable<IProfession> SecondaryProfessions
        {
            get { return dataService.SecondaryProfessions; }
        }

        public bool? ShowValidBuilds
        {
            get { return showValidBuilds; }
            set
            {
                if(showValidBuilds == value)
                    return;

                showValidBuilds = value;
                SendPropertyChanged("ShowValidBuilds");
            }
        }


        public bool SearchAnyPrimaryProfession
        {
            get { return searchAnyPrimaryProfession; }
            set
            {
                if(searchAnyPrimaryProfession == value)
                    return;

                searchAnyPrimaryProfession = value;
                SendPropertyChanged("SearchAnyPrimaryProfession");
            }
        }


        public bool SearchAnySecondaryProfession
        {
            get { return searchAnySecondaryProfession; }
            set
            {
                if(searchAnySecondaryProfession == value)
                    return;

                searchAnySecondaryProfession = value;
                SendPropertyChanged("SearchAnySecondaryProfession");
            }
        }


        public IList<IProfession> SelectedSecondaryProfessions
        {
            get { return selectedSecondaryProfessions; }
            set
            {
                if(selectedSecondaryProfessions == value)
                    return;

                selectedSecondaryProfessions = value;
                SendPropertyChanged("SelectedSecondaryProfessions");
            }
        }


        public IList<IProfession> SelectedPrimaryProfessions
        {
            get { return selectedPrimaryProfessions; }
            set
            {
                if(selectedPrimaryProfessions == value)
                    return;

                selectedPrimaryProfessions = value;
                SendPropertyChanged("SelectedPrimaryProfessions");
            }
        }


        public string SearchName
        {
            get { return searchName; }
            set
            {
                if(searchName == value)
                    return;

                searchName = value;
                SendPropertyChanged("SearchName");
            }
        }


        public string SearchTags
        {
            get { return searchTags; }
            set
            {
                if(searchTags == value)
                    return;

                searchTags = value;
                SendPropertyChanged("SearchTags");
            }
        }

        public string SearchNotes
        {
            get { return searchNotes; }
            set
            {
                if(searchNotes == value)
                    return;

                searchNotes = value;
                SendPropertyChanged("SearchNotes");
            }
        }


        public string SearchAuthor
        {
            get { return searchAuthor; }
            set
            {
                if(searchAuthor == value)
                    return;

                searchAuthor = value;
                SendPropertyChanged("SearchAuthor");
            }
        }

        public static ViewDetails ViewDetails
        {
            get { return viewDetails; }
        }

        #region ISkillsViewModel Members

        public ISkillsView View
        {
            get { return view; }
        }

        public ICollectionView Builds
        {
            get { return collectionView; }
            set
            {
                if(collectionView == value)
                    return;

                collectionView = value;
                SendPropertyChanged("Builds");
            }
        }

        public ICommand SearchCommand { get; private set; }

        public ICommand ResetCommand { get; private set; }

        public string HeaderText
        {
            get { return ViewDetails.Name; }
        }

        #endregion

        protected override void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var defaultView = CollectionViewSource.GetDefaultView(service.AllTemplates);
            defaultView.Filter = MatchBuild;

            e.Result = defaultView;
        }

        protected override void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            var result = args.Result as ICollectionView;
            if(result == null)
                return;

            Builds = result;
        }

        private void GenerateCommands()
        {
            SearchCommand = new DelegateCommand(OnSearch);
            ResetCommand = new DelegateCommand(ResetFilters);
        }

        private void ResetFilters()
        {
            ShowValidBuilds = null;
            SearchAnyPrimaryProfession = true;
            SearchAnySecondaryProfession = true;
            SelectedPrimaryProfessions = new List<IProfession>();
            SelectedSecondaryProfessions = new List<IProfession>();
            SearchNotes = string.Empty;
            SearchName = string.Empty;
            SearchTags = string.Empty;
            SearchAuthor = string.Empty;

            RefreshBuilds();
        }


        private void OnSearch()
        {
            RefreshBuilds();
        }

        private void RefreshBuilds()
        {
            if(collectionView != null)
                collectionView.Refresh();
        }

        private void ServiceBuildsChanged(object sender, EventArgs e)
        {
            RunWorkerASync();
        }

        private bool MatchBuild(object buildObject)
        {
            var build = buildObject as SkillTemplate;

            if(build == null)
                return false;

            if(!IsValid(build, SearchName, i => i.Name))
                return false;

            if(!IsValid(build, SearchAuthor, i => i.Author))
                return false;

            if(!IsValid(build, SearchTags, i => i.Tags))
                return false;

            if(!IsValid(build, SearchNotes, i => i.Notes))
                return false;

            if(ShowValidBuilds.HasValue && ShowValidBuilds.Value != build.IsValid)
                return false;

            if(!SearchAnySecondaryProfession && !SelectedSecondaryProfessions.Contains(build.SecondaryProfession))
                return false;

            if(!SearchAnyPrimaryProfession && !SelectedPrimaryProfessions.Contains(build.PrimaryProfession))
                return false;

            return true;
        }

        private static bool IsValid(SkillTemplate build, string searchValue, Func<SkillTemplate, string> valueSelector)
        {
            if(string.IsNullOrEmpty(searchValue))
                return true;

            return valueSelector(build).ToLower().Contains(searchValue.ToLower());
        }
    }
}