using System;
using System.Collections.Generic;

namespace TemplateManager.Infrastructure.Controllers
{
    public interface IViewManager
    {
        IEnumerable<ViewDetails> GetViewsForRegion(string region);
        void Register(ViewDetails viewDetails, string region, Func<IHeadedContent> view);
        void OpenView(string viewKey);
    }
}