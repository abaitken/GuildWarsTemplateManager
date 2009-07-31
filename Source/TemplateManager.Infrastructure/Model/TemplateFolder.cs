using System.Collections.Generic;

namespace TemplateManager.Infrastructure.Model
{
    public class TemplateFolder
    {
        private readonly string folderPath;
        private readonly IEnumerable<SkillTemplate> templates;
        private readonly IEnumerable<TemplateFolder> subFolders;

        public TemplateFolder(string folderPath, IEnumerable<SkillTemplate> templates, IEnumerable<TemplateFolder> subFolders)
        {
            this.folderPath = folderPath;
            this.templates = templates;
            this.subFolders = subFolders;
        }

        public IEnumerable<TemplateFolder> SubFolders
        {
            get { return subFolders; }
        }

        public IEnumerable<SkillTemplate> Templates
        {
            get { return templates; }
        }

        public string FolderPath
        {
            get { return folderPath; }
        }
    }
}
