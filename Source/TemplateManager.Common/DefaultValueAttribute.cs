using System;

namespace TemplateManager.Common
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class DefaultValueAttribute : Attribute
    {
        private readonly object defaultValue;

        public DefaultValueAttribute(object defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        public DefaultValueAttribute(DefaultValueType defaultValueType)
        {
            switch (defaultValueType)
            {
                case DefaultValueType.EmptyString:
                    defaultValue = string.Empty;
                    break;
                case DefaultValueType.Null:
                    defaultValue = null;
                    break;
                default:
                    throw new InvalidOperationException("Unexpected default value type");
            }
        }

        public object DefaultValue
        {
            get { return defaultValue; }
        }
    }
    public enum DefaultValueType
    {
        EmptyString,
        Null
    }
}
