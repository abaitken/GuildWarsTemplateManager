using System;
using System.Windows;

namespace TemplateManager.Common.WindowPositionManager
{
    public static class WindowExtensions
    {
        public static readonly DependencyProperty WindowSettingsProperty =
            DependencyProperty.RegisterAttached("WindowSettings",
                                                typeof(Type),
                                                typeof(WindowExtensions),
                                                new PropertyMetadata(OnWindowSettingsChanged));

        public static Type GetWindowSettings(DependencyObject obj)
        {
            return (Type) obj.GetValue(WindowSettingsProperty);
        }

        public static void SetWindowSettings(DependencyObject obj, Type value)
        {
            obj.SetValue(WindowSettingsProperty, value);
        }

        private static void OnWindowSettingsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var window = obj as Window;

            if(window == null)
                return;

            var type = e.NewValue as Type;

            if(type == null)
                return;

            var settings = Activator.CreateInstance(type) as IWindowSettings;

            if(settings == null)
                return;

            var provider = new WindowSettingsProvider(window, settings);
            provider.Attach();
        }
    }
}