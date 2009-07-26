using System;
using System.ComponentModel;
using System.Windows;

namespace TemplateManager.WindowPositionManager
{
    /// <summary>
    /// Persists a Window's Size, Location and WindowState to UserScopeSettings 
    /// </summary>
    public class WindowSettings
    {
        #region Constructor

        private readonly Window window;

        public WindowSettings(Window window)
        {
            this.window = window;
        }

        #endregion

        #region Attached "Save" Property Implementation

        /// <summary>
        /// Register the "Save" attached property and the "OnSaveInvalidated" callback 
        /// </summary>
        public static readonly DependencyProperty SaveProperty
            = DependencyProperty.RegisterAttached("Save",
                                                  typeof(bool),
                                                  typeof(WindowSettings),
                                                  new FrameworkPropertyMetadata(
                                                      new PropertyChangedCallback(OnSaveInvalidated)));

        public static void SetSave(DependencyObject dependencyObject, bool enabled)
        {
            dependencyObject.SetValue(SaveProperty, enabled);
        }

        /// <summary>
        /// Called when Save is changed on an object.
        /// </summary>
        private static void OnSaveInvalidated(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var window = dependencyObject as Window;
            if(window != null)
            {
                if((bool) e.NewValue)
                {
                    var settings = new WindowSettings(window);
                    settings.Attach();
                }
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Load the Window Size Location and State from the settings object
        /// </summary>
        protected virtual void LoadWindowState()
        {
            Settings.Reload();
            if(Settings.Location != Rect.Empty)
            {
                window.Left = Settings.Location.Left;
                window.Top = Settings.Location.Top;
                window.Width = Settings.Location.Width;
                window.Height = Settings.Location.Height;
            }

            if(Settings.WindowState != WindowState.Maximized)
            {
                window.WindowState = Settings.WindowState;
            }
        }


        /// <summary>
        /// Save the Window Size, Location and State to the settings object
        /// </summary>
        protected virtual void SaveWindowState()
        {
            Settings.WindowState = window.WindowState;
            Settings.Location = window.RestoreBounds;
            Settings.Save();
        }

        #endregion

        #region Private Methods

        private void Attach()
        {
            if(window != null)
            {
                window.Closing += WindowClosing;
                window.Initialized += WindowInitialized;
                window.Loaded += WindowLoaded;
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if(Settings.WindowState == WindowState.Maximized)
            {
                window.WindowState = Settings.WindowState;
            }
        }

        private void WindowInitialized(object sender, EventArgs e)
        {
            LoadWindowState();
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            SaveWindowState();
        }

        #endregion

        #region Settings Property Implementation

        private IWindowApplicationSettings windowApplicationSettings;

        [Browsable(false)]
        public IWindowApplicationSettings Settings
        {
            get
            {
                if(windowApplicationSettings == null)
                    windowApplicationSettings = CreateWindowApplicationSettingsInstance();

                return windowApplicationSettings;
            }
        }

        protected virtual IWindowApplicationSettings CreateWindowApplicationSettingsInstance()
        {
            return new WindowApplicationSettings();
        }

        #endregion
    }
}