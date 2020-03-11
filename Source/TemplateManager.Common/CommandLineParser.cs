using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;

namespace TemplateManager.Common
{
    public class CommandLineParser
    {
        private readonly StringDictionary parameters;

        private CommandLineParser()
        {
            parameters = new StringDictionary();
        }

        private string this[string param]
        {
            get
            {
                var key = param.ToLower();
                return parameters[key];
            }
            set
            {
                var key = param.ToLower();
                parameters[key] = value;
            }
        }

        public bool Contains(string[] options)
        {
            return options.Any(ContainsKey);
        }

        public int Count
        {
            get { return parameters.Count; }
        }

        private bool ContainsKey(string parameter)
        {
            var key = parameter.ToLower();
            return parameters.ContainsKey(key);
        }

        public bool Contains(string param)
        {
            return ContainsKey(param);
        }

        public bool HasValue(string[] options)
        {
            return GetValue(options) != defaultValue;
        }

        private const string defaultValue = "#!DEFAULTVALUE!#";

        public static CommandLineParser Parse(IEnumerable<string> args)
        {
            var parameters = new CommandLineParser();
            var foundParameter = false;
            var previousKey = string.Empty;

            // The args that are passed from a .NET app are split on spaces or by quotes
            //ie. prog.exe -p bob       will end up as:     -p, bob
            // proge.exe -p "a b c"     will end up as:     -p, a b c
            // As it also removes quotes
            // prog.exe "-p a b c"      will end up as:     -p, a b c

            // These patterns will allow for:
            // {-,/,--}param
            // {-,/,--}param{ ,=,:}(")value(")

            // Examples: 
            // -param1 value1 --param2 /param3:"Test-:-work" 
            //   /param4=happy -param5 '--=nice=--'
            // However the following will not work:
            // -p ' " '
            // or -p 'something other'
            // Single quotes should only be used within double quotes, 
            // and although you can have a single value within single quotes, you cannot include a double quote 

            var parameterRegex = new Regex(@"^(?:--|/|-)(?<Param>[\w-]+)$", RegexOptions.Compiled);
            var parameterWithValueRegex = new Regex(@"^(?:--|/|-)(?<Param>[\w-]+)[,=:\s]""?(?<Value>.+?)""?$",
                                                    RegexOptions.Compiled);

            foreach (var arg in args)
            {
                var match = parameterWithValueRegex.Match(arg);

                if (match.Success)
                {
                    foundParameter = false;

                    var value = match.Groups["Value"].Value;

                    // Due to an issue with the regex, it can match arguments like:
                    // -param:""
                    // And picks up the last quote in the value match
                    if (value.Equals(@""""))
                        value = string.Empty;

                    var key = match.Groups["Param"].Value;

                    if (parameters.Contains(key))
                        parameters[key] = value;
                    else
                        parameters.Add(key, value);

                    previousKey = string.Empty;
                    continue;
                }

                match = parameterRegex.Match(arg);

                if (match.Success)
                {
                    foundParameter = true;

                    var key = match.Groups["Param"].Value;

                    if (parameters.Contains(key))
                        parameters[key] = defaultValue;
                    else
                        parameters.Add(key, defaultValue);

                    previousKey = key;
                    continue;
                }

                if (foundParameter)
                {
                    foundParameter = false;

                    var value = arg;
                    var key = previousKey;

                    if (parameters.Contains(key))
                        parameters[key] = value;
                    else
                        parameters.Add(key, value);


                    previousKey = string.Empty;
                    continue;
                }

                // Otherwise what we have is a value without a parameter
            }

            return parameters;
        }

        private void Add(string key, string value)
        {
            parameters.Add(key.ToLower(), value);
        }

        public string GetValue(string param)
        {
            return this[param];
        }

        public string GetValueOrDefault(string param, string defaultValue)
        {
            return Contains(param) ? GetValue(param) : defaultValue;
        }

        public string GetValueOrDefault(string[] options, string defaultValue)
        {
            var result = GetValue(options);

            if (!string.IsNullOrEmpty(result))
                return result;

            return defaultValue;
        }

        public string GetValue(IEnumerable<string> options)
        {
            var results = from item in options
                          where Contains(item)
                          let result = GetValue(item)
                          select result;

            return results.First();
        }
    }
}
