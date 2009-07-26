using System;

namespace TemplateManager.Infrastructure.Model
{
    public struct SkillAttribute
    {
        private readonly string name;
        private readonly int nativeId;
        private readonly bool isPrimary;

        public SkillAttribute(string name, int nativeId, bool isPrimary)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            this.name = name;
            this.nativeId = nativeId;
            this.isPrimary = isPrimary;
        }

        public int NativeId { get { return nativeId; } }
        public string Name { get { return name; } }
        public bool IsPrimary { get { return isPrimary; } }

        public override string ToString()
        {
            return string.Format("{0}", name);
        }
    }
}