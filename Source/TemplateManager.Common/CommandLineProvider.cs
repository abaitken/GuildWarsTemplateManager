using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TemplateManager.Common
{

    public class CommandLineProvider<T> where T : class, ICommandLineOptions
    {
        public T LoadFromFile(string path)
        {
            var result = XmlSerializationHelper.LoadFromFile<T>(path);
            result.IsValid = true;
            return result;
        }

        public T CreateArgumentsModel(CommandLineParser parser)
        {
            var type = typeof(T);
            var items = GetPropertyAttributeMappings(type);

            if (items.Count == 0)
                throw new InvalidOperationException("No command line attributes were found");

            var result = (T)Activator.CreateInstance(type);
            var iface = (ICommandLineOptions)result;

            var errors = new List<string>();

            foreach (var item in items)
            {
                var property = item.Property;
                var options = item.Options;

                if (options.Length == 0)
                    throw new InvalidOperationException(string.Format("No options were found for property '{0}'", property.Name));

                var exists = parser.Contains(options);

                if (!exists && item.Optional)
                {
                    property.SetValue(result, item.DefaultValue);
                    continue;
                }

                if (!exists)
                {
                    errors.Add(string.Format("Missing argument: {0} ({1})", options.First(), item.Name));
                    continue;
                }

                if (property.PropertyType == typeof(bool))
                {
                    property.SetValue(result, exists);
                    continue;
                }

                var value = parser.GetValue(options);

                if (property.PropertyType == typeof(string))
                {
                    property.SetValue(result, value);
                    continue;
                }

                if (property.PropertyType == typeof(int))
                {
                    int parsedValue;
                    if (!int.TryParse(value, out parsedValue))
                    {
                        errors.Add(string.Format("Argument '{0} ({1})' is not a number", options.First(), item.Name));
                        continue;
                    }

                    property.SetValue(result, parsedValue);
                    continue;
                }

                throw new InvalidOperationException("Unexpected property type");
            }

            result.ParsingErrors = errors;
            iface.IsValid = errors.Count == 0;

            return result;
        }

        private static List<PropertyAttributeMapping> GetPropertyAttributeMappings(Type type)
        {
            var properties = type.GetProperties();

            var query = from item in properties
                        let helpTextItems = item.GetCustomAttributes<HelpTextAttribute>(true)
                        where helpTextItems.Count() != 0
                        let options = item.GetCustomAttributes<OptionAttribute>(true)
                        let defaultValues = item.GetCustomAttributes<DefaultValueAttribute>(true)
                        let defaultValue = defaultValues.DefaultIfEmpty(null).First()
                        let optional = defaultValues.Count() != 0
                        let helpText = helpTextItems.First()
                        select new PropertyAttributeMapping
                        {
                            Property = item,
                            HelpText = helpText.HelpText,
                            Name = helpText.Name,
                            Syntax = helpText.Syntax,
                            Options = options.Select(i => i.Option).ToArray(),
                            DefaultValue = optional ? defaultValue.DefaultValue : null,
                            Optional = optional
                        };

            return query.ToList();
        }

        private struct PropertyAttributeMapping
        {
            public PropertyInfo Property { get; set; }
            public string Name { get; set; }
            public string HelpText { get; set; }
            public string Syntax { get; set; }
            public string[] Options { get; set; }
            public object DefaultValue { get; set; }
            public bool Optional { get; set; }
        }

        public string CreateHelpText()
        {
            var type = typeof(T);
            var items = GetPropertyAttributeMappings(type);

            if (items.Count == 0)
                throw new InvalidOperationException("No command line attributes were found");

            var result = new StringBuilder();

            foreach (var item in items)
            {
                var options = item.Options;

                foreach (var option in options)
                {
                    var syntax = string.IsNullOrEmpty(item.Syntax)
                                     ? item.Syntax
                                     : string.Format(" {0}", item.Syntax);

                    result.AppendFormat(" -{0}{1}", option, syntax);
                    result.AppendLine();
                }
                result.AppendFormat("   {0}{1}",
                                    item.Optional ? "(Optional) " : string.Empty,
                                    item.HelpText);
                result.AppendLine();
                result.AppendLine();
            }

            return result.ToString();
        }
    }
}
