using System;
using System.IO;
using InfiniteRain.Shared.FileSystem;
using Microsoft.Win32;

namespace TemplateManager.Services
{
    internal class TemplateStoreLocator
    {
        public static string FindBuildStorePath()
        {
            var result = LookInDocumentsFolder();

            if(!string.IsNullOrEmpty(result))
                return result;

            return LookInGuildWarsInstallFolder();
        }

        private static string LookInGuildWarsInstallFolder()
        {
            var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ArenaNet\Guild Wars");

            if(key == null)
                return string.Empty;

            var installPath = Path.GetDirectoryName(key.GetValue("Path").ToString());

            if(string.IsNullOrEmpty(installPath))
                return string.Empty;

            var result = FolderHelper.CreatePathFromParts(installPath, "Templates", "Skills");

            if(Directory.Exists(result))
                return result;

            return string.Empty;
        }

        private static string LookInDocumentsFolder()
        {
            var docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var result = FolderHelper.CreatePathFromParts(docs, "Guild Wars", "Templates", "Skills");

            if(Directory.Exists(result))
                return result;

            return string.Empty;
        }
    }
}