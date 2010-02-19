using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TemplateManager.Infrastructure.Model
{
    public class SkillTemplate
    {
        private readonly string buildFile;
        private readonly IProfession primaryProfession;
        private readonly IProfession secondaryProfession;
        private readonly IList<ISkill> skills;
        private readonly IEnumerable<AttributeValue> attributes;
        private string author;
        private string tags;
        private string notes;
        private bool metaLoaded;
        private readonly bool isInvalid;

        public SkillTemplate(string buildFile, IProfession emptyProfession)
            : this(buildFile, emptyProfession, emptyProfession, Enumerable.Empty<ISkill>().ToList(), Enumerable.Empty<AttributeValue>())
        {
            isInvalid = true;
        }

        public SkillTemplate(string buildFile, IProfession primaryProfession, IProfession secondaryProfession, IList<ISkill> skills, IEnumerable<AttributeValue> attributes)
        {
            if (string.IsNullOrEmpty(buildFile))
                throw new ArgumentNullException("buildFile");

            if (primaryProfession == null)
                throw new ArgumentNullException("primaryProfession");

            if (secondaryProfession == null)
                throw new ArgumentNullException("secondaryProfession");

            if (skills == null)
                throw new ArgumentNullException("skills");

            if (attributes == null)
                throw new ArgumentNullException("attributes");

            this.buildFile = buildFile;
            this.primaryProfession = primaryProfession;
            this.secondaryProfession = secondaryProfession;
            this.skills = skills;
            this.attributes = attributes;
        }

        public string BuildFile { get { return buildFile; } }
        public string Name { get { return Path.GetFileNameWithoutExtension(buildFile); } }
        public IProfession PrimaryProfession { get { return primaryProfession; } }
        public IProfession SecondaryProfession { get { return secondaryProfession; } }
        public IList<ISkill> Skills { get { return skills; } }
        public IEnumerable<AttributeValue> Attributes { get { return attributes; } }

        public bool IsInvalid { get { return isInvalid; } }
       
        public string Author
        {
            get
            {
                ReadMeta();
                return author;
            }
        }

        public string Tags
        {
            get
            {
                ReadMeta();
                return tags;
            }
        }

        public string Notes
        {
            get
            {
                ReadMeta();
                return notes;
            }
        }

        private void ReadMeta()
        {
            if (metaLoaded)
                return;

            var meta = MetaLoader.LoadMetaFromBuildFile(buildFile);
            author = meta.Author;
            tags = meta.Tags;
            notes = meta.Notes;

            metaLoaded = true;
        }

        private int skillKey;

        public int SkillKey
        {
            get
            {
                if (skillKey != 0)
                    return skillKey;

                var orderedResult = from skill in Skills
                                    orderby skill.TemplateId
                                    select skill.TemplateId;

                skillKey = HashCodeBuilder.Build(orderedResult);

                return skillKey;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}