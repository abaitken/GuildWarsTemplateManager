using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace TemplateManager.Common
{
    public class CommandLineParser
    {
        private readonly StringDictionary parameters;

        private CommandLineParser(StringDictionary parameters)
        {
            this.parameters = parameters;
        }


        public bool this[string param]
        {
            get { return parameters.ContainsKey(param); }
        }

        public bool this[CommandLineOption param]
        {
            get { return parameters.ContainsKey(param.LongName) || parameters.ContainsKey(param.ShortName); }
        }

        public int Count
        {
            get { return parameters.Count; }
        }

        public static CommandLineParser Parse(IEnumerable<string> args)
        {
            var parameters = new StringDictionary();
            var spliter = new Regex(@"^-{1,2}|^/|=|:",
                                    RegexOptions.IgnoreCase | RegexOptions.Compiled);

            var remover = new Regex(@"^['""]?(.*?)['""]?$",
                                    RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string parameter = null;
            string[] parts;

            // Valid parameters forms:
            // {-,/,--}param{ ,=,:}((",')value(",'))
            // Examples: 
            // -param1 value1 --param2 /param3:"Test-:-work" 
            //   /param4=happy -param5 '--=nice=--'
            foreach(var txt in args)
            {
                // Look for new parameters (-,/ or --) and a
                // possible enclosed value (=,:)
                parts = spliter.Split(txt, 3);

                switch(parts.Length)
                {
                        // Found a value (for the last parameter 
                        // found (space separator))
                    case 1:
                        if(parameter != null)
                        {
                            if(!parameters.ContainsKey(parameter))
                            {
                                parts[0] =
                                    remover.Replace(parts[0], "$1");

                                parameters.Add(parameter, parts[0]);
                            }
                            parameter = null;
                        }
                        // else Error: no parameter waiting for a value (skipped)
                        break;

                        // Found just a parameter
                    case 2:
                        // The last parameter is still waiting. 
                        // With no value, set it to true.
                        if(parameter != null)
                        {
                            if(!parameters.ContainsKey(parameter))
                                parameters.Add(parameter, "true");
                        }
                        parameter = parts[1];
                        break;

                        // Parameter with enclosed value
                    case 3:
                        // The last parameter is still waiting. 
                        // With no value, set it to true.
                        if(parameter != null)
                        {
                            if(!parameters.ContainsKey(parameter))
                                parameters.Add(parameter, "true");
                        }

                        parameter = parts[1];

                        // Remove possible enclosing characters (",')
                        if(!parameters.ContainsKey(parameter))
                        {
                            parts[2] = remover.Replace(parts[2], "$1");
                            parameters.Add(parameter, parts[2]);
                        }

                        parameter = null;
                        break;
                }
            }
            // In case a parameter is still waiting
            if(parameter != null)
            {
                if(!parameters.ContainsKey(parameter))
                    parameters.Add(parameter, "true");
            }

            return new CommandLineParser(parameters);
        }

        public string GetValue(string param)
        {
            return parameters[param];
        }

        public string GetValueOrDefault(string param, string defaultValue)
        {
            return this[param] ? GetValue(param) : defaultValue;
        }
    }
}