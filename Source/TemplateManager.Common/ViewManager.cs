using Prism.Regions;
using System;
using System.Linq;
using System.Collections.Generic;

namespace TemplateManager.Common
{
    public class ViewManager
    {
        private readonly IRegionManager regionManager;
        private readonly Dictionary<string, View> registeredViews = new Dictionary<string, View>();

        public ViewManager(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public IEnumerable<ViewDetails> GetViewsForCategory(string category)
        {
            var result = from item in registeredViews
                         let view = item.Value
                         let detail = view.Detail
                         where detail.Category == category
                         select detail;
            return result;
        }

        public void Register(ViewDetails viewDetails, string region, Func<IHeadedContent> view)
        {
            var item = new View
            {
                Detail = viewDetails,
                ViewDelegate = view,
                Region = region
            };

            registeredViews.Add(viewDetails.Name, item);
        }

        public bool ActivateView(ViewDetails details)
        {
            var registeredView = FindView(details.Name);

            if (!registeredView.HasValue)
                return false;

            var region = regionManager.Regions[registeredView.Value.Region];
            var view = region.GetView(details.Name);

            if (view == null)
                return false;

            region.Activate(view);
            return true;
        }

        public void OpenView(ViewDetails details)
        {
            var registeredView = registeredViews[details.Name];

            var region = regionManager.Regions[registeredView.Region];

            var content = registeredView.ViewDelegate();

            region.Add(content, details.Name);
            region.Activate(content);
        }

        private View? FindView(string key)
        {
            View result;
            if (!registeredViews.TryGetValue(key, out result))
                return null;

            return result;
        }

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
