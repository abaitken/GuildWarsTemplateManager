using System;
using System.IO;
using Microsoft.Win32;
using TemplateManager.Common.FileSystem;

namespace TemplateManager.Options
{
    public static class BuildStoreLocator
    {
        public static string FindBuildStorePath()
        {
            var docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var skillsFolder = FolderHelper.CreatePathFromParts(docs, "Guild Wars", "Templates", "Skills");

            if (Directory.Exists(skillsFolder))
                return skillsFolder;

            var regLocator = new RegistryLocator();
            if (!string.IsNullOrEmpty(regLocator.GuildWarsFolder))
            {
                skillsFolder = FolderHelper.CreatePathFromParts(regLocator.GuildWarsFolder, "Templates", "Skills");

                if (Directory.Exists(skillsFolder))
                    return skillsFolder;
            }

            return string.Empty;
        }

        #region Nested type: RegistryLocator

        private class RegistryLocator
        {
            public RegistryLocator()
            {
                var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ArenaNet\Guild Wars");

                if (key == null)
                    return;

                GuildWarsFolder = Path.GetDirectoryName(key.GetValue("Path").ToString());
            }

            public string GuildWarsFolder { get; private set; }
        }

        #endregion
    }
}