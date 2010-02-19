using System.Collections.Generic;

namespace TemplateManager.Modules.DataExplorer.Presentation.DataExplorer
{
    partial class DataExplorerViewModel
    {
        private bool? elite;
        private bool? isPvp;
        private bool? pvEOnly;
        private bool? causesExhaustion;
        private KeyValuePair<string, string> selectedActivationTime;
        private KeyValuePair<string, string> selectedAdrenalineCost;
        private KeyValuePair<string, string> selectedAreaOfEffect;
        private KeyValuePair<string, string> selectedAttribute;
        private KeyValuePair<string, string> selectedCampaign;
        private KeyValuePair<string, string> selectedEnergyCost;
        private KeyValuePair<string, string> selectedProfession;
        private KeyValuePair<string, string> selectedProjectile;
        private KeyValuePair<string, string> selectedRange;
        private KeyValuePair<string, string> selectedRechargeTime;
        private KeyValuePair<string, string> selectedSacrificeCost;
        private KeyValuePair<string, string> selectedSkillType;
        private KeyValuePair<string, string> selectedSpecialType;
        private KeyValuePair<string, string> selectedTarget;
        private KeyValuePair<string, string> selectedUpkeepValue;

        #region IMainWindowViewModel Members


        bool searchForTemplateId;
        public bool SearchForTemplateId
        {
            get { return searchForTemplateId; }
            set
            {
                if (searchForTemplateId == value)
                    return;

                searchForTemplateId = value;
                SendPropertyChanged("SearchForTemplateId");
            }
        }


        int selectedTemplateId;
        public int SelectedTemplateId
        {
            get { return selectedTemplateId; }
            set
            {
                if (selectedTemplateId == value)
                    return;

                selectedTemplateId = value;
                SendPropertyChanged("SelectedTemplateId");
            }
        }


        public KeyValuePair<string, string> SelectedAreaOfEffect
        {
            get { return selectedAreaOfEffect; }
            set
            {
                if (selectedAreaOfEffect.Value == value.Value)
                    return;

                selectedAreaOfEffect = value;
                SendPropertyChanged("SelectedAreaOfEffect");
            }
        }


        public bool? PvEOnly
        {
            get { return pvEOnly; }
            set
            {
                if (pvEOnly == value)
                    return;

                pvEOnly = value;
                SendPropertyChanged("PvEOnly");
            }
        }

        public bool? IsPvP
        {
            get { return isPvp; }
            set
            {
                if (isPvp == value)
                    return;

                isPvp = value;
                SendPropertyChanged("IsPvP");
            }
        }

        public KeyValuePair<string, string> SelectedProfession
        {
            get { return selectedProfession; }
            set
            {
                if (selectedProfession.Value == value.Value)
                    return;

                selectedProfession = value;
                SendPropertyChanged("SelectedProfession");
            }
        }

        public KeyValuePair<string, string> SelectedRange
        {
            get { return selectedRange; }
            set
            {
                if (selectedRange.Value == value.Value)
                    return;

                selectedRange = value;
                SendPropertyChanged("SelectedRange");
            }
        }

        public KeyValuePair<string, string> SelectedTarget
        {
            get { return selectedTarget; }
            set
            {
                if (selectedTarget.Value == value.Value)
                    return;

                selectedTarget = value;
                SendPropertyChanged("SelectedTarget");
            }
        }

        public KeyValuePair<string, string> SelectedProjectile
        {
            get { return selectedProjectile; }
            set
            {
                if (selectedProjectile.Value == value.Value)
                    return;

                selectedProjectile = value;
                SendPropertyChanged("SelectedProjectile");
            }
        }

        public KeyValuePair<string, string> SelectedAttribute
        {
            get { return selectedAttribute; }
            set
            {
                if (selectedAttribute.Value == value.Value)
                    return;

                selectedAttribute = value;
                SendPropertyChanged("SelectedAttribute");
            }
        }

        public KeyValuePair<string, string> SelectedSkillType
        {
            get { return selectedSkillType; }
            set
            {
                if (selectedSkillType.Value == value.Value)
                    return;

                selectedSkillType = value;
                SendPropertyChanged("SelectedSkillType");
            }
        }

        public KeyValuePair<string, string> SelectedSpecialType
        {
            get { return selectedSpecialType; }
            set
            {
                if (selectedSpecialType.Value == value.Value)
                    return;

                selectedSpecialType = value;
                SendPropertyChanged("SelectedSpecialType");
            }
        }

        public bool? CausesExhaustion
        {
            get { return causesExhaustion; }
            set
            {
                if (causesExhaustion == value)
                    return;

                causesExhaustion = value;
                SendPropertyChanged("CausesExhaustion");
            }
        }

        public bool? Elite
        {
            get { return elite; }
            set
            {
                if (elite == value)
                    return;

                elite = value;
                SendPropertyChanged("Elite");
            }
        }

        public KeyValuePair<string, string> SelectedCampaign
        {
            get { return selectedCampaign; }
            set
            {
                if (selectedCampaign.Value == value.Value)
                    return;

                selectedCampaign = value;
                SendPropertyChanged("SelectedCampaign");
            }
        }

        public KeyValuePair<string, string> SelectedActivationTime
        {
            get { return selectedActivationTime; }
            set
            {
                if (selectedActivationTime.Value == value.Value)
                    return;

                selectedActivationTime = value;
                SendPropertyChanged("SelectedActivationTime");
            }
        }

        public KeyValuePair<string, string> SelectedRechargeTime
        {
            get { return selectedRechargeTime; }
            set
            {
                if (selectedRechargeTime.Value == value.Value)
                    return;

                selectedRechargeTime = value;
                SendPropertyChanged("SelectedRechargeTime");
            }
        }

        public KeyValuePair<string, string> SelectedEnergyCost
        {
            get { return selectedEnergyCost; }
            set
            {
                if (selectedEnergyCost.Value == value.Value)
                    return;

                selectedEnergyCost = value;
                SendPropertyChanged("SelectedEnergyCost");
            }
        }

        public KeyValuePair<string, string> SelectedSacrificeCost
        {
            get { return selectedSacrificeCost; }
            set
            {
                if (selectedSacrificeCost.Value == value.Value)
                    return;

                selectedSacrificeCost = value;
                SendPropertyChanged("SelectedSacrificeCost");
            }
        }

        public KeyValuePair<string, string> SelectedAdrenalineCost
        {
            get { return selectedAdrenalineCost; }
            set
            {
                if (selectedAdrenalineCost.Value == value.Value)
                    return;

                selectedAdrenalineCost = value;
                SendPropertyChanged("SelectedAdrenalineCost");
            }
        }

        public KeyValuePair<string, string> SelectedUpkeepValue
        {
            get { return selectedUpkeepValue; }
            set
            {
                if (selectedUpkeepValue.Value == value.Value)
                    return;

                selectedUpkeepValue = value;
                SendPropertyChanged("SelectedUpkeepValue");
            }
        }


        IList<KeyValuePair<string, string>> selectedRemovesValues;
        public IList<KeyValuePair<string, string>> SelectedRemovesValues
        {
            get { return selectedRemovesValues; }
            set
            {
                if (selectedRemovesValues == value)
                    return;

                selectedRemovesValues = value;
                SendPropertyChanged("SelectedRemovesValues");
            }
        }


        #endregion
    }
}
