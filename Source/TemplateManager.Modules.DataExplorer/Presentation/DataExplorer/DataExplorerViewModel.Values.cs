using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplateManager.Modules.DataExplorer.Presentation.DataExplorer
{
    partial class DataExplorerViewModel
    {
        private IDictionary<string, string> activationTimes;
        private IDictionary<string, string> adrenalineCosts;
        private IDictionary<string, string> areaOfEffectValues;
        private IDictionary<string, string> attributes;
        private IDictionary<string, string> campaigns;
        private IDictionary<string, string> energyCosts;
        private IDictionary<string, string> professions;
        private IDictionary<string, string> projectiles;
        private IDictionary<string, string> ranges;
        private IDictionary<string, string> rechargeTimes;
        private IDictionary<string, string> sacrificeCosts;
        private IDictionary<string, string> skillTypes;
        private IDictionary<string, string> specialTypes;
        private IDictionary<string, string> targets;
        private IDictionary<string, string> upkeepValues;

        #region IMainWindowViewModel Members

        public IDictionary<string, string> Attributes
        {
            get { return attributes; }
            private set
            {
                if (attributes == value)
                    return;

                attributes = value;
                SendPropertyChanged("Attributes");
            }
        }


        public IDictionary<string, string> AreaOfEffectValues
        {
            get { return areaOfEffectValues; }
            set
            {
                if (areaOfEffectValues == value)
                    return;

                areaOfEffectValues = value;
                SendPropertyChanged("AreaOfEffectValues");
            }
        }


        public IDictionary<string, string> SkillTypes
        {
            get { return skillTypes; }
            private set
            {
                if (skillTypes == value)
                    return;

                skillTypes = value;
                SendPropertyChanged("SkillTypes");
            }
        }


        public IDictionary<string, string> SpecialTypes
        {
            get { return specialTypes; }
            private set
            {
                if (specialTypes == value)
                    return;

                specialTypes = value;
                SendPropertyChanged("SpecialTypes");
            }
        }

        public IDictionary<string, string> Professions
        {
            get { return professions; }
            private set
            {
                if (professions == value)
                    return;
                professions = value;
                SendPropertyChanged("Professions");
            }
        }

        public IDictionary<string, string> ActivationTimes
        {
            get { return activationTimes; }
            private set
            {
                if (activationTimes == value)
                    return;

                activationTimes = value;
                SendPropertyChanged("ActivationTimes");
            }
        }

        public IDictionary<string, string> RechargeTimes
        {
            get { return rechargeTimes; }
            private set
            {
                if (rechargeTimes == value)
                    return;

                rechargeTimes = value;
                SendPropertyChanged("RechargeTimes");
            }
        }

        public IDictionary<string, string> EnergyCosts
        {
            get { return energyCosts; }
            private set
            {
                if (energyCosts == value)
                    return;
                energyCosts = value;
                SendPropertyChanged("EnergyCosts");
            }
        }

        public IDictionary<string, string> SacrificeCosts
        {
            get { return sacrificeCosts; }
            private set
            {
                if (sacrificeCosts == value)
                    return;
                sacrificeCosts = value;
                SendPropertyChanged("SacrificeCosts");
            }
        }

        public IDictionary<string, string> AdrenalineCosts
        {
            get { return adrenalineCosts; }
            private set
            {
                if (adrenalineCosts == value)
                    return;
                adrenalineCosts = value;
                SendPropertyChanged("AdrenalineCosts");
            }
        }

        public IDictionary<string, string> UpkeepValues
        {
            get { return upkeepValues; }
            private set
            {
                if (upkeepValues == value)
                    return;
                upkeepValues = value;
                SendPropertyChanged("UpkeepValues");
            }
        }

        public IDictionary<string, string> Campaigns
        {
            get { return campaigns; }
            private set
            {
                if (campaigns == value)
                    return;

                campaigns = value;
                SendPropertyChanged("Campaigns");
            }
        }

        public IDictionary<string, string> Ranges
        {
            get { return ranges; }
            private set
            {
                if (ranges == value)
                    return;

                ranges = value;
                SendPropertyChanged("Ranges");
            }
        }

        public IDictionary<string, string> Targets
        {
            get { return targets; }
            private set
            {
                if (targets == value)
                    return;

                targets = value;
                SendPropertyChanged("Targets");
            }
        }

        public IDictionary<string, string> Projectiles
        {
            get { return projectiles; }
            private set
            {
                if (projectiles == value)
                    return;

                projectiles = value;
                SendPropertyChanged("Projectiles");
            }
        }


        IDictionary<string, string> removesValues;
        public IDictionary<string, string> RemovesValues
        {
            get { return removesValues; }
            set
            {
                if (removesValues == value)
                    return;

                removesValues = value;
                SendPropertyChanged("RemovesValues");
            }
        }


        #endregion
    }
}
