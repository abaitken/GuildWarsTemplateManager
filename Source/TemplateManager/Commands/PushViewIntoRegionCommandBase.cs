using System;
using System.Windows.Input;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using TemplateManager.Common.CommandModel;

namespace TemplateManager.Commands
{

    abstract class PushViewIntoRegionCommandBase : CommandModelBase
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        protected PushViewIntoRegionCommandBase(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        protected IUnityContainer Container
        {
            get { return container; }
        }

        public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var regionName = e.Parameter as string;

            if(string.IsNullOrEmpty(regionName))
                throw new InvalidOperationException("Expected parameter to be a string");

            var view = GetView();

            var region = regionManager.Regions[regionName];

            if(!region.Views.Contains(view))
                region.Add(view);

            region.Activate(view);
        }

        private object GetView()
        {
            if (viewReference != null && viewReference.Target != null)
                return viewReference.Target;

            var result = View;

            viewReference = new WeakReference(result);
            
            return result;
        }

        private WeakReference viewReference;

        protected abstract object View { get; }
    }
}