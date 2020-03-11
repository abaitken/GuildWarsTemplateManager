using System;

namespace TemplateManager.Common
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class HelpTextAttribute : Attribute
    {
        private readonly string name;
        private readonly string helpText;
        private readonly string syntax;

        public HelpTextAttribute(string name)
            : this(name, string.Empty, string.Empty)
        {
        }

        public HelpTextAttribute(string name, string helpText)
            : this(name, helpText, string.Empty)
        {
        }

        public HelpTextAttribute(string name, string helpText, string syntax)
        {
            this.name = name;
            this.helpText = helpText;
            this.syntax = syntax;
        }

        public string Name
        {
            get { return name; }
        }

        public string HelpText
        {
            get { return helpText; }
        }

        public string Syntax
        {
            get { return syntax; }
        }
    }
}
