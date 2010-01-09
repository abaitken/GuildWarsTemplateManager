using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using System.Xml;
using TemplateManager.Common.FileSystem;
using TemplateManager.Infrastructure;

namespace TemplateManager.Modules.Themes
{
    class ThemeManager : IThemeManager
    {
        private readonly IApplicationSettings applicationSettings;

        private static class Const
        {
            public const string ThemeDirectoryName = "Themes";
            public const string DefaultThemeName = "[Default]";
        }

        private ResourceDictionary currentlyLoadedTheme;

        public ThemeManager(IApplicationSettings applicationSettings)
        {
            this.applicationSettings = applicationSettings;
            ApplyTheme(applicationSettings.Theme);

            applicationSettings.ThemeChanged += OnThemeChange;
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
            var themePath = FolderHelper.CreatePathFromParts(AppDomain.CurrentDomain.BaseDirectory,
                                                             Const.ThemeDirectoryName,
                                                             themeName,
                                                             "Theme.xaml");

            var reader = XmlReader.Create(themePath);

            return XamlReader.Load(reader) as ResourceDictionary;
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

        private void OnThemeChange(object sender, EventArgs e)
        {
            // TODO : This event could send the new value
            ApplyTheme(applicationSettings.Theme);
        }

    }
}