using System.Collections.Generic;
using TemplateManager.Common.CommandModel;
using TemplateManager.Infrastructure.Events;
using TemplateManager.Infrastructure.Interfaces;
using TemplateManager.Infrastructure.Model;

namespace TemplateManager.SearchView
{
    public interface ISearchViewModel : IHeadedContent
    {
        ISearchView View { get; }
        IEnumerable<Profession> PrimaryProfessions { get; }
        IEnumerable<Profession> SecondaryProfessions { get; }
        ICommandModel SearchCommand { get; }
        ICommandModel ResetCommand { get; }
        SearchParameters SearchParameters { get; set; }
    }
}