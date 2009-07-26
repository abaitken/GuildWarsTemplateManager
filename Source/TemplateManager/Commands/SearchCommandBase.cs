using System.Windows.Input;
using Microsoft.Practices.Composite.Events;
using TemplateManager.Common.CommandModel;
using TemplateManager.Infrastructure.Events;
using TemplateManager.SearchView;

namespace TemplateManager.Commands
{
    public abstract class SearchCommandBase : CommandModelBase
    {
        private readonly IEventAggregator eventAggregator;

        protected SearchCommandBase(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var model = (ISearchViewModel)e.Parameter;
            UpdateModel(model);

            eventAggregator.GetEvent<SkillTemplateFilterEvent>().Publish(GetEventPayload(model));
            
        }

        protected abstract SearchParameters GetEventPayload(ISearchViewModel model);
        protected virtual void UpdateModel(ISearchViewModel model)
        {
            // Left blank intentionally
        }
    }
}