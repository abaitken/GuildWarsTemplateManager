using System;

namespace TemplateManager.Common
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class OptionAttribute : Attribute
    {
        private readonly string option;

        public OptionAttribute(string option)
        {
            this.option = option;
        }

        public string Option
        {
            get { return option; }
        }
    }
}
