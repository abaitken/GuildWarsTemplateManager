using Microsoft.Practices.Composite.Events;
using TemplateManager.Infrastructure.Events;
using TemplateManager.SearchView;

namespace TemplateManager.Commands
{
    public class ResetCommand : SearchCommandBase
    {
        public ResetCommand(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
        }

        protected override SearchParameters GetEventPayload(ISearchViewModel model)
        {
            return null;
        }

        protected override void UpdateModel(ISearchViewModel model)
        {
            model.SearchParameters = new SearchParameters();
        }
    }
}