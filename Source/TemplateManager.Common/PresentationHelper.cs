using System;
using System.Windows;
using Unity;

namespace TemplateManager.Common
{
    public static class PresentationHelper
    {
        public static void ShowViewDialog<TModel>(this IUnityContainer container, Func<TModel, object> viewSelector)
        {
            ShowViewDialog(container, viewSelector, Application.Current.MainWindow);
        }

        public static void ShowViewDialog<TModel>(this IUnityContainer container,
                                                  Func<TModel, object> viewSelector,
                                                  Window ownerWindow)
        {
            var model = container.Resolve<TModel>();
            var viewWindow = viewSelector(model) as Window;

            if (viewWindow == null)
                throw new InvalidOperationException("Expected view to be a window");

            viewWindow.Owner = ownerWindow;
            viewWindow.ShowDialog();
        }
    }
}
