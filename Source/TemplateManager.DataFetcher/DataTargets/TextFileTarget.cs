using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TemplateManager.DataFetcher.Logging;
using TemplateManager.DataFetcher.Model;

namespace TemplateManager.DataFetcher.DataTargets
{
    internal class TextFileTarget : IDataTarget
    {
        private readonly ILogger logger;
        private readonly string path;

        public TextFileTarget(ILogger logger, string path)
        {
            this.logger = logger;
            this.path = path;
        }

        #region IDataTarget Members

        public void Update(IEnumerable<Skill> data)
        {
            logger.Log(GetType(), "Writing skills to disk", LogSeverity.InformationHigh);

            foreach(var skill in data)
            {
                File.AppendAllText(path, CreateText(skill));
                File.AppendAllText(path, '-'.Repeat(60));
            }

            logger.Log(GetType(), "Update complete", LogSeverity.InformationHigh);
        }

        #endregion

        private static string CreateText(Skill skill)
        {
            var result = new StringBuilder();

            result.AppendLine(GetSkillText(skill));

            return result.ToString();
        }

        private static string GetSkillText(Skill skill)
        {
            var type = typeof(Skill);

            var properties = type.GetProperties();

            var values = new Dictionary<string, string>();

            foreach(var info in properties)
            {
                var value = info.GetValue(skill, null);

                if(value == null)
                    continue;

                var listValue = value is string ? null : value as IEnumerable;

                if(listValue != null)
                {
                    var valueBuilder = new StringBuilder();
                    valueBuilder.AppendLine("Collection");

                    var containsValues = false;

                    foreach(var item in listValue)
                    {
                        valueBuilder.AppendLine(string.Format("\t{0}", item));
                        containsValues = true;
                    }

                    if(containsValues)
                        values.Add(info.Name, valueBuilder.ToString());
                }
                else
                {
                    values.Add(info.Name, value.ToString());
                }
            }

            var maxKey = values.Keys.Max(i => i.Length);

            var result = new StringBuilder();

            const string format = "{0}{1} = {2}";

            foreach(var value in values)
            {
                var paddingRequired = (maxKey - value.Key.Length) + 3;

                result.AppendLine(string.Format(format, value.Key, ' '.Repeat(paddingRequired), value.Value));
            }

            return result.ToString();
        }
    }

    internal static class StringExtensions
    {
        public static string Repeat(this char item, int times)
        {
            return new string(Enumerable.Repeat(item, times).ToArray());
        }
    }
}