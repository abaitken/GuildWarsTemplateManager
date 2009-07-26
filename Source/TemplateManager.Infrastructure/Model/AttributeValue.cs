namespace TemplateManager.Infrastructure.Model
{
    public struct AttributeValue
    {
        private readonly SkillAttribute attribute;
        private readonly int value;

        public AttributeValue(SkillAttribute attribute, int value)
        {
            this.attribute = attribute;
            this.value = value;
        }

        public SkillAttribute Attribute { get { return attribute; } }
        public int Value { get { return value; } }

        public override string ToString()
        {
            return string.Format("{0}, {1}", attribute, value);
        }
    }
}