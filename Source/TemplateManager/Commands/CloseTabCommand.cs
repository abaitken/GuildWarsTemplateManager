using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Regions;
using TemplateManager.Common.CommandModel;

namespace TemplateManager.Commands
{
    class CloseTabCommand : CommandModelBase
    {
        private readonly IRegionManager regionManager;

        public CloseTabCommand(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var tabItem = (TabItem)e.Parameter;
            var tabControl = (TabControl)e.Source;

            var regionName = RegionManager.GetRegionName(tabControl);

            regionManager.Regions[regionName].Remove(tabItem.Content);
        }
    }
}