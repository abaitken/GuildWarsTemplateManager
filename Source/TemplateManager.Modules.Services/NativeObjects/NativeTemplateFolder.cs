using System.Collections.Generic;
using TemplateParser.Skills;

namespace TemplateManager.Modules.Services.NativeObjects
{
    internal class NativeTemplateFolder
    {
        private readonly string folderPath;
        private readonly IEnumerable<KeyValuePair<string, NativeSkillBuild>> templates;
        private readonly IEnumerable<NativeTemplateFolder> subFolders;

        public NativeTemplateFolder(IEnumerable<KeyValuePair<string, NativeSkillBuild>> builds, string folderPath, IEnumerable<NativeTemplateFolder> subFolders)
        {
            this.templates = builds;
            this.subFolders = subFolders;
            this.folderPath = folderPath;
        }

        public IEnumerable<NativeTemplateFolder> SubFolders
        {
            get { return subFolders; }
        }

        public string FolderPath
        {
            get { return folderPath; }
        }

        public IEnumerable<KeyValuePair<string, NativeSkillBuild>> Templates
        {
            get { return templates; }
        }
    }
}