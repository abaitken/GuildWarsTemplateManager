using System.IO;
using System.Xml.Linq;
using TemplateManager.Infrastructure.Extensions;

namespace TemplateManager.Infrastructure.Model
{
    internal class MetaLoader
    {
        private static readonly MetaLoader empty = new NullObject();

        private MetaLoader()
        {
            Author = string.Empty;
            Tags = string.Empty;
            Notes = string.Empty;
        }

        public static MetaLoader Empty
        {
            get { return empty; }
        }

        public string Author { get; set; }
        public string Tags { get; set; }
        public string Notes { get; set; }

        public static MetaLoader LoadMetaFromBuildFile(string buildFile)
        {
            var metaPath = GetMetaPathFromBuildPath(buildFile);

            if (!File.Exists(metaPath))
                return Empty;

            var xdoc = XDocument.Load(metaPath);
            var root = xdoc.Element("Meta");
            return new MetaLoader
                       {
                           Author = root.Element("Author").GetValueOrDefault(),
                           Notes = root.Element("Notes").GetValueOrDefault(),
                           Tags = root.Element("Tags").GetValueOrDefault()
                       };
        }

        private static string GetMetaPathFromBuildPath(string buildFile)
        {
            return Path.Combine(Path.GetDirectoryName(buildFile),
                                string.Format("{0}.{1}", Path.GetFileNameWithoutExtension(buildFile), "xml"));
        }

        #region Nested type: NullObject

        private class NullObject : MetaLoader
        {
        }

        #endregion
    }
}