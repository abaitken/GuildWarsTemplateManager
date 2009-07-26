using System.Collections.Generic;
using Microsoft.Practices.Composite.Events;
using TemplateManager.Commands;
using TemplateManager.Common.CommandModel;
using TemplateManager.Common.ViewModel;
using TemplateManager.Infrastructure.Events;
using TemplateManager.Infrastructure.Model;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.SearchView
{
    internal class SearchViewModel : ViewModelBase, ISearchViewModel
    {
        private readonly IDataService service;
        private readonly ISearchView view;
        private SearchParameters searchParameters = new SearchParameters();

        public SearchViewModel(ISearchView view, IDataService service, IEventAggregator eventAggregator)
        {
            this.view = view;
            this.service = service;
            view.Model = this;
            GenerateCommands(eventAggregator);
        }

        #region ISearchViewModel Members

        public ISearchView View
        {
            get { return view; }
        }

        public IEnumerable<Profession> PrimaryProfessions
        {
            get { return service.PrimaryProfessions; }
        }

        public IEnumerable<Profession> SecondaryProfessions
        {
            get { return service.SecondaryProfessions; }
        }

        public ICommandModel SearchCommand { get; private set; }
        public ICommandModel ResetCommand { get; private set; }

        public SearchParameters SearchParameters
        {
            get { return searchParameters; }
            set
            {
                searchParameters = value;
                SendPropertyChanged("SearchParameters");
            }
        }

        public string HeaderText
        {
            get { return "Search Options"; }
        }

        #endregion

        private void GenerateCommands(IEventAggregator aggregator)
        {
            SearchCommand = new SearchCommand(aggregator);
            ResetCommand = new ResetCommand(aggregator);
        }
    }
}