using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TemplateParser.Skills;

namespace TemplateManager.Modules.Services.NativeObjects
{
    internal class NativeBuildFactory
    {
        private static readonly NativeBuildFactory empty = new NullObject();

        private readonly string buildStore;
        private readonly SkillBuildParser parser;

        private NativeBuildFactory()
        {
        }

        public NativeBuildFactory(string buildStore)
        {
            if (string.IsNullOrEmpty(buildStore))
                throw new ArgumentNullException("buildStore");

            this.buildStore = buildStore;
            parser = new SkillBuildParser();
        }

        public static NativeBuildFactory Empty
        {
            get { return empty; }
        }

        public virtual NativeTemplateFolder TemplateFolder
        {
            get { return ReadTemplates(); }
        }

        private NativeTemplateFolder ReadTemplates()
        {
            return GetTemplateFolder(buildStore);
        }

        private IEnumerable<KeyValuePair<string, NativeSkillBuild>> GetTemplatesInFolder(string folder)
        {
            return from template in Directory.GetFiles(folder, "*.txt", SearchOption.TopDirectoryOnly)
                   select new KeyValuePair<string, NativeSkillBuild>(template, ParseBuildFile(template));
        }

        private NativeTemplateFolder GetTemplateFolder(string folder)
        {
            var templates = GetTemplatesInFolder(folder);
            var subFolders = from subfolder in Directory.GetDirectories(folder)
                             select GetTemplateFolder(subfolder);

            return new NativeTemplateFolder(templates, folder, subFolders);
        }

        private NativeSkillBuild ParseBuildFile(string filePath)
        {
            var templateCode = File.ReadAllText(filePath);

            return parser.ParseTemplateCode(templateCode);
        }

        public static NativeBuildFactory Create(string buildStore)
        {
            if (string.IsNullOrEmpty(buildStore))
                return Empty;

            return new NativeBuildFactory(buildStore);
        }

        #region Nested type: NullObject

        private class NullObject : NativeBuildFactory
        {
            public override NativeTemplateFolder TemplateFolder
            {
                get { return new NativeTemplateFolder(new Dictionary<string, NativeSkillBuild>(), "Unknown", new List<NativeTemplateFolder>()); }
            }
        }

        #endregion
    }
}