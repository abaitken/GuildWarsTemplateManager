using System;
using System.Text.RegularExpressions;

namespace TemplateManager.DataFetcher.Parsers
{
    internal static class GreenTemplateParser
    {
        // Matches {{gr|1|2}} wiki templates. 
        // Also creates groups for characters before and after the numbers: {{gr|+1|2%}}
        private const string pattern =
            @"\{\{(?<Type>gr|gr2)\|(?<BeforeFirst>.*?)(?<First>[0-9]+)(?<AfterFirst>.*?)\|(?<BeforeSecond>.*?)(?<Second>[0-9]+)(?<AfterSecond>.*?)[|]{0,1}\}\}";

        public static string Parse(string text)
        {
            var result = text;

            var matchResult = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

            foreach(Match match in matchResult)
            {
                var middleSection = string.Empty;

                var beforeFirst = match.Groups["BeforeFirst"].Value;
                var first = double.Parse(match.Groups["First"].Value);
                var afterFirst = match.Groups["AfterFirst"].Value;

                var beforeSecond = match.Groups["BeforeSecond"].Value;
                var second = double.Parse(match.Groups["Second"].Value);
                var afterSecond = match.Groups["AfterSecond"].Value;

                if(match.Groups["Type"].Value.ToLower() == "gr")
                {
                    var middle = Math.Round(first + 12 * (second - first) / 15);

                    middleSection = string.Format("...{0}{1}", middle, afterSecond);
                }

                var firstSection = string.Format("{0}{1}{2}", beforeFirst, first, afterFirst);
                var secondSection = string.Format("...{0}{1}{2}", beforeSecond, second, afterSecond);

                var replacement = string.Format("{0}{1}{2}", firstSection, middleSection, secondSection);

                result = result.Replace(match.Value, replacement);
            }

            return result;
        }
    }
}