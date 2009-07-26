using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using TemplateManager.Properties;

namespace TemplateManager
{
    class ThemeManager : IThemeManager
    {
        private static class Const
        {
            public const string ThemeDirectoryName = "Themes";
            public const string DefaultThemeName = "[Default]";
        }

        private ResourceDictionary currentlyLoadedTheme;

        public ThemeManager()
        {
            ApplyTheme(Settings.Default.Theme);

            Settings.Default.PropertyChanged += PropertyChanged;
        }

        public IEnumerable<string> AvailableThemes
        {
            get
            {
                var defaultTheme = Enumerable.Repeat(Const.DefaultThemeName, 1);
                var result = from directory in Directory.GetDirectories(Const.ThemeDirectoryName)
                             select new DirectoryInfo(directory).Name;


                return defaultTheme.Union(result);
            }
        }

        private static ResourceDictionary GetThemeResourceDictionary(string themeName)
        {
            var packUri = string.Format(CultureInfo.InvariantCulture, "/{0}/{1}/Theme.xaml", Const.ThemeDirectoryName, themeName);
            return Application.LoadComponent(new Uri(packUri, UriKind.Relative)) as ResourceDictionary;
        }

        private void ApplyTheme(string themeName)
        {
            RemoveTheme();

            if (string.IsNullOrEmpty(themeName) || themeName == Const.DefaultThemeName)
                return;

            var dictionary = GetThemeResourceDictionary(themeName);

            if (dictionary == null)
                return; // TODO : Look at throwing here

            currentlyLoadedTheme = dictionary;
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }

        private void RemoveTheme()
        {
            if (currentlyLoadedTheme != null)
                Application.Current.Resources.MergedDictionaries.Remove(currentlyLoadedTheme);

            currentlyLoadedTheme = null;
        }

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Theme")
                return;

            ApplyTheme(Settings.Default.Theme);
        }

    }
}