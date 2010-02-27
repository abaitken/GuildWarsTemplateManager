using System;
using System.Collections.Generic;
using System.Linq;

namespace TemplateParser.Skills
{
    /// <summary>
    /// A skill build
    /// </summary>
    public class NativeSkillBuild
    {
        private const int SkillLimit = 8;

        #region fields

        private readonly SortedList<int, KeyValuePair<int, int>> attributes;
        private readonly SortedList<int, int> skills;

        #endregion

        #region constructors

        /// <summary>
        /// Initialise the build
        /// </summary>
        public NativeSkillBuild()
        {
            attributes = new SortedList<int, KeyValuePair<int, int>>();
            skills = new SortedList<int, int>();
        }

        #endregion

        #region properties

        /// <summary>
        /// Primary profession
        /// </summary>
        public int PrimaryProfessionId { get; set; }

        /// <summary>
        /// Secondary profession
        /// </summary>
        public int SecondaryProfessionId { get; set; }

        /// <summary>
        /// A list of attributes and thier associated value
        /// </summary>
        public IEnumerable<KeyValuePair<int, int>> AttributeIdValuePairs
        {
            get { return attributes.Values; }
        }

        /// <summary>
        /// A list of skills
        /// </summary>
        public IEnumerable<int> SkillIds
        {
            get { return skills.Values; }
        }

        public string TemplateCode { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Add an attribute and value
        /// </summary>
        /// <param name="attributeId">Attribute</param>
        /// <param name="value">Attribute value</param>
        public void AddAttribute(int attributeId, int value)
        {
            var items =
                from attributePair in AttributeIdValuePairs
                where attributePair.Key == attributeId
                select attributePair;

            if(items.Count() > 0)
                throw new NotSupportedException();

            var key = new KeyValuePair<int, int>(attributeId, value);

            attributes.Add(attributes.Count, key);
        }

        /// <summary>
        /// Add a skill
        /// </summary>
        /// <param name="skillId">Skill to add</param>
        public void AddSkill(int skillId)
        {
            if(skillId != 0 && skills.ContainsValue(skillId))
                return;

            if(skills.Count > SkillLimit)
                throw new InvalidOperationException();

            skills.Add(skills.Count, skillId);
        }

        #endregion
    }
}