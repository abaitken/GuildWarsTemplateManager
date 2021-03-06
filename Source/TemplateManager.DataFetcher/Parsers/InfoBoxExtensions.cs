using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TemplateManager.DataFetcher.Parsers
{
    internal static class InfoBoxExtensions
    {
        public static string GetRawTextValue(this IDictionary<string, string> source, string key)
        {
            if(!source.ContainsKey(key))
                return null;

            var result = source[key].Trim();

            if(string.IsNullOrEmpty(result))
                return null;

            return result;
        }

        public static string GetTextValue(this IDictionary<string, string> source, string key)
        {
            var result = source.GetRawTextValue(key);

            if(result == null)
                return null;

            result = Regex.Replace(result, @"<!--.*?-->", string.Empty, RegexOptions.Singleline);
            result = Regex.Replace(result, @"[^a-zA-Z0-9!"".?+\-*/,= \[\]\\]", string.Empty);
            return result;
        }

        public static bool? GetBoolValue(this IDictionary<string, string> source, string key)
        {
            var result = source.GetTextValue(key);

            if(string.IsNullOrEmpty(result))
                return null;

            result = result.ToLower();

            return (result == "y" || result == "yes" || result == "true");
        }

        public static List<string> GetInfoBoxValueArray(this IDictionary<string, string> source,
                                                        IEnumerable<string> keys)
        {
            var result = new List<string>();

            foreach(var key in keys)
            {
                var value = source.GetTextValue(key);

                if(string.IsNullOrEmpty(value))
                    continue;

                result.Add(value);
            }

            return result;
        }

        public static int? GetIntegerValue(this IDictionary<string, string> source, string key)
        {
            var result = source.GetTextValue(key);

            if(string.IsNullOrEmpty(result))
                return null;

            var match = Regex.Match(result, "^[^0-9.]*(?<Result>[0-9.]+)");

            if(!match.Success)
                return null;

            return int.Parse(match.Groups["Result"].Value);
        }

        public static double? GetDoubleValue(this IDictionary<string, string> source, string key)
        {
            var result = source.GetTextValue(key);

            if(string.IsNullOrEmpty(result))
                return null;

            var match = Regex.Match(result, "^[^0-9.]*(?<Result>[0-9.]+)");

            if(!match.Success)
                return null;

            return double.Parse(match.Groups["Result"].Value);
        }

        public static double? GetActivationTime(this IDictionary<string, string> source)
        {
            var result = source.GetTextValue("activation");

            if(string.IsNullOrEmpty(result))
                return null;


            result = result.Replace("{{", string.Empty);
            result = result.Replace("}}", string.Empty);

            switch(result)
            {
                case "1/4":
                    return 0.25;
                case "1/2":
                    return 0.5;
                case "2/3":
                    return 0.66;
                case "3/4":
                case "¾":
                    return 0.75;
            }

            return double.Parse(result);
        }
    }
}