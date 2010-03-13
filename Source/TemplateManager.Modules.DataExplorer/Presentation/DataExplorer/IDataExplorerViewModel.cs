using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace TemplateManager.Modules.DataExplorer.Presentation.DataExplorer
{
    public interface IDataExplorerViewModel
    {
        IDataExplorerView View { get; }
        ICommand ResetFiltersCommand { get; }
        ICommand RefreshCommand { get; }
        ICollectionView Skills { get; }
        IDictionary<string, string> Attributes { get; }
        IDictionary<string, string> AreaOfEffectValues { get; set; }
        IDictionary<string, string> SkillTypes { get; }
        IDictionary<string, string> SpecialTypes { get; }
        IDictionary<string, string> Professions { get; }
        IDictionary<string, string> ActivationTimes { get; }
        IDictionary<string, string> RechargeTimes { get; }
        IDictionary<string, string> EnergyCosts { get; }
        IDictionary<string, string> SacrificeCosts { get; }
        IDictionary<string, string> AdrenalineCosts { get; }
        IDictionary<string, string> UpkeepValues { get; }
        IDictionary<string, string> Campaigns { get; }
        IDictionary<string, string> Ranges { get; }
        IDictionary<string, string> Targets { get; }
        IDictionary<string, string> Projectiles { get; }
        IDictionary<string, string> RemovesValues { get; set; }
        bool SearchForTemplateId { get; set; }
        int SelectedTemplateId { get; set; }
        KeyValuePair<string, string> SelectedAreaOfEffect { get; set; }
        bool? PvEOnly { get; set; }
        bool? IsPvP { get; set; }
        KeyValuePair<string, string> SelectedProfession { get; set; }
        KeyValuePair<string, string> SelectedRange { get; set; }
        KeyValuePair<string, string> SelectedTarget { get; set; }
        KeyValuePair<string, string> SelectedProjectile { get; set; }
        KeyValuePair<string, string> SelectedAttribute { get; set; }
        KeyValuePair<string, string> SelectedSkillType { get; set; }
        KeyValuePair<string, string> SelectedSpecialType { get; set; }
        bool? CausesExhaustion { get; set; }
        bool? Elite { get; set; }
        KeyValuePair<string, string> SelectedCampaign { get; set; }
        KeyValuePair<string, string> SelectedActivationTime { get; set; }
        KeyValuePair<string, string> SelectedRechargeTime { get; set; }
        KeyValuePair<string, string> SelectedEnergyCost { get; set; }
        KeyValuePair<string, string> SelectedSacrificeCost { get; set; }
        KeyValuePair<string, string> SelectedAdrenalineCost { get; set; }
        KeyValuePair<string, string> SelectedUpkeepValue { get; set; }
        IList<KeyValuePair<string, string>> SelectedRemovesValues { get; set; }
        string HeaderText { get; }
        void OnViewLoaded();
    }
}