using Microsoft.Practices.Composite.Events;
using TemplateManager.Infrastructure.Events;
using TemplateManager.SearchView;

namespace TemplateManager.Commands
{
    public class SearchCommand : SearchCommandBase
    {
        public SearchCommand(IEventAggregator eventAggregator) 
            : base(eventAggregator)
        {
        }

        protected override SearchParameters GetEventPayload(ISearchViewModel model)
        {
            return model.SearchParameters;
        }
    }
}