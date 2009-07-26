namespace TemplateManager.Common
{
    public struct CommandLineOption
    {
        private readonly string longName;
        private readonly string shortName;

        public CommandLineOption(string longName, string shortName)
        {
            this.longName = longName;
            this.shortName = shortName;
        }

        public string LongName
        {
            get { return longName; }
        }

        public string ShortName
        {
            get { return shortName; }
        }
    }
}