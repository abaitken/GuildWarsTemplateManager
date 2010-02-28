using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Composite.Regions;
using TemplateManager.Infrastructure;
using TemplateManager.Infrastructure.Controllers;

namespace TemplateManager.Modules.Workspace
{
    internal class ViewManager : IViewManager
    {
        private readonly IRegionManager regionManager;
        private readonly Dictionary<string, View> registeredViews = new Dictionary<string, View>();

        public ViewManager(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        #region IViewManager Members

        public IEnumerable<ViewDetails> GetViewsForRegion(string region)
        {
            return from item in registeredViews
                   let view = item.Value
                   where view.Region == region
                   select view.Detail;
        }

        public void Register(ViewDetails viewDetails, string region, Func<IHeadedContent> view)
        {
            var item = new View
                           {
                               Detail = viewDetails,
                               ViewDelegate = view,
                               Region = region
                           };

            registeredViews.Add(viewDetails.Key, item);
        }

        public void OpenView(string viewKey)
        {
            var view = registeredViews[viewKey];
            var content = view.ViewDelegate();
            regionManager.AddToRegion(view.Region, content);
            regionManager.Regions[view.Region].Activate(content);
        }

        #endregion

        #region Nested type: View

        private struct View
        {
            public ViewDetails Detail { get; set; }
            public string Region { get; set; }
            public Func<IHeadedContent> ViewDelegate { get; set; }
        }

        #endregion
    }
}