using System.Collections.ObjectModel;
using TemplateManager.Infrastructure.Model;

namespace TemplateManager.Infrastructure.Events
{
    public class SearchParameters
    {
        public SearchParameters()
        {
            ShowValidBuilds = true;
            ShowInvalidBuilds = true;
            AnyPrimaryProfession = true;
            AnySecondaryProfession = true;
            

            PrimaryProfessions = new ObservableCollection<IProfession>();
            SecondaryProfessions = new ObservableCollection<IProfession>();
        }

        public string Name { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        public string Notes { get; set; }
        public bool ShowInvalidBuilds { get; set; }
        public bool ShowValidBuilds { get; set; }
        public bool AnyPrimaryProfession { get; set; }
        public ObservableCollection<IProfession> PrimaryProfessions { get; set; }
        public bool AnySecondaryProfession { get; set; }
        public ObservableCollection<IProfession> SecondaryProfessions { get; set; }
    }
}