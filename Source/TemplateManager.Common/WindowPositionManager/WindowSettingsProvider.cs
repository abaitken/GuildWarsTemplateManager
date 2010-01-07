using System;
using System.ComponentModel;
using System.Windows;

namespace TemplateManager.Common.WindowPositionManager
{
    /// <summary>
    /// Persists a Window's Size, Location and WindowState to UserScopeSettings 
    /// </summary>
    internal class WindowSettingsProvider
    {
        private readonly Window window;
        private readonly IWindowSettings settings;

        public WindowSettingsProvider(Window window, IWindowSettings settings)
        {
            this.window = window;
            this.settings = settings;
        }

        /// <summary>
        /// Load the Window Size Location and State from the settings object
        /// </summary>
        private void LoadWindowState()
        {
            settings.Reload();

            if(settings.Location != Rect.Empty)
            {
                window.Left = settings.Location.Left;
                window.Top = settings.Location.Top;
                window.Width = settings.Location.Width;
                window.Height = settings.Location.Height;
            }

            if(settings.WindowState != WindowState.Maximized)
                window.WindowState = settings.WindowState;
        }


        /// <summary>
        /// Save the Window Size, Location and State to the settings object
        /// </summary>
        private void SaveWindowState()
        {
            settings.WindowState = window.WindowState;
            settings.Location = window.RestoreBounds;
            settings.Save();
        }


        public void Attach()
        {
            if(window == null)
                return;

            window.Closing += WindowClosing;
            window.Initialized += WindowInitialized;
            window.Loaded += WindowLoaded;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if(settings.WindowState == WindowState.Maximized)
                window.WindowState = settings.WindowState;
        }

        private void WindowInitialized(object sender, EventArgs e)
        {
            LoadWindowState();
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            SaveWindowState();
        }
        
    }
}