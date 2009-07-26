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

        public virtual IDictionary<string, NativeSkillBuild> Builds
        {
            get { return ReadBuilds(); }
        }

        private IDictionary<string, NativeSkillBuild> ReadBuilds()
        {
            var result = from buildFilePath in Directory.GetFiles(buildStore, "*.txt", SearchOption.AllDirectories)
                         select new
                                    {
                                        Path = buildFilePath,
                                        Build = ParseBuildFile(buildFilePath)
                                    };

            return result.ToDictionary(k => k.Path, v => v.Build);
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
            public override IDictionary<string, NativeSkillBuild> Builds
            {
                get { return new Dictionary<string, NativeSkillBuild>(); }
            }
        }

        #endregion
    }
}