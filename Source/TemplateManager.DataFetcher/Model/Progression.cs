using System.Collections.Generic;

namespace TemplateManager.DataFetcher.Model
{
    public class Progression
    {
        public string Attribute { get; set; }
        public IEnumerable<ProgressionVar> Vars { get; set; }
        public string MaxLevel { get; set; }
    }
}