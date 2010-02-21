namespace TemplateManager.Infrastructure.Model
{
    public struct AttributeValue
    {
        private readonly IAttribute attribute;
        private readonly int value;

        public AttributeValue(IAttribute attribute, int value)
        {
            this.attribute = attribute;
            this.value = value;
        }

        public IAttribute Attribute
        {
            get { return attribute; }
        }

        public int Value
        {
            get { return value; }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", attribute.Name, value);
        }
    }
}