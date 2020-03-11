using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using TemplateManager.Common;
using TemplateManager.Infrastructure;
using TemplateManager.Infrastructure.Model;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.Modules.DataExplorer.Presentation.DataExplorer
{
    internal partial class DataExplorerViewModel : BackgroundLoadingViewModel, IDataExplorerViewModel
    {
        private const string any = "<Any>";
        private static readonly KeyValuePair<string, string> anyPair = new KeyValuePair<string, string>(any, any);
        private static readonly ViewDetails viewDetails = new ViewDetails("Data Explorer", ToolCategories.View);
        private readonly IDataService service;
        private readonly IDataExplorerView view;
        private ICollectionView skills;

        public DataExplorerViewModel(IDataExplorerView view, IDataService service)
        {
            this.view = view;
            this.service = service;
            ResetFilters();

            GenerateCommands();

            view.Model = this;
        }

        public static ViewDetails ViewDetails
        {
            get { return viewDetails; }
        }

        #region IDataExplorerViewModel Members

        public ICommand ResetFiltersCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        public ICollectionView Skills
        {
            get { return skills; }
            private set
            {
                if(skills == value)
                    return;

                skills = value;
                SendPropertyChanged("Skills");
            }
        }

        public IDataExplorerView View
        {
            get { return view; }
        }

        public string HeaderText
        {
            get { return ViewDetails.Name; }
        }

        #endregion

        protected override void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var result = CollectionViewSource.GetDefaultView(service.Skills);
            result.Filter = SkillFilter;

            e.Result = result;
        }

        protected override void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            var result = args.Result as ICollectionView;
            if(result == null)
                return;

            DataProviderLoadComplete();
            Skills = result;
        }

        private void GenerateCommands()
        {
            ResetFiltersCommand = new DelegateCommand(ResetFilters);
            RefreshCommand = new DelegateCommand(RefreshSkills);
        }

        private void RefreshSkills()
        {
            if(Skills != null)
                Skills.Refresh();
        }

        private void ResetFilters()
        {
            SelectedAttribute = anyPair;
            SelectedSkillType = anyPair;
            SelectedSpecialType = anyPair;
            SelectedProfession = anyPair;
            SelectedTarget = anyPair;
            SelectedProjectile = anyPair;
            SelectedRange = anyPair;
            SelectedCampaign = anyPair;
            SelectedActivationTime = anyPair;
            SelectedRechargeTime = anyPair;
            SelectedEnergyCost = anyPair;
            SelectedSacrificeCost = anyPair;
            SelectedAdrenalineCost = anyPair;
            SelectedUpkeepValue = anyPair;
            Elite = null;
            PvEOnly = null;
            CausesExhaustion = null;
            SelectedAreaOfEffect = anyPair;
            SelectedRemovesValues = new List<KeyValuePair<string, string>>();

            RefreshSkills();
        }

        private void DataProviderLoadComplete()
        {
            Attributes = SelectValues(i => i.Attribute.Name);
            SkillTypes = SelectValues(i => i.Type);
            SpecialTypes = SelectValues(i => i.SpecialType);
            Professions = SelectValues(i => i.Profession.Name);
            Campaigns = SelectValues(i => i.Campaign);
            Ranges = SelectValues(i => i.Range);
            Projectiles = SelectValues(i => i.Projectile);
            Targets = SelectValues(i => i.Target);
            ActivationTimes = SelectValues(i => i.ActivationTime.ToString());
            RechargeTimes = SelectValues(i => i.RechargeTime.ToString());
            EnergyCosts = SelectValues(i => i.EnergyCost.ToString());
            SacrificeCosts = SelectValues(i => i.Sacrifice.ToString());
            AdrenalineCosts = SelectValues(i => i.AdrenalineCost.ToString());
            UpkeepValues = SelectValues(i => i.UpkeepCost.ToString());
            AreaOfEffectValues = SelectValues(i => i.AreaOfEffect);
            RemovesValues = SelectValues(i => i.Removes);
        }

        private IDictionary<string, string> SelectValues(Func<ISkill, IEnumerable<string>> selector)
        {
            var values = from item in service.Skills
                         let selectedItems = selector(item)
                         let selectedValues = selectedItems ?? Enumerable.Empty<string>()
                         from value in selectedValues
                         let compareValue = string.IsNullOrEmpty(value) ? value : value.ToLower().Trim()
                         group value by compareValue
                         into g
                             select new
                                        {
                                            Key = string.IsNullOrEmpty(g.Key) ? "<None>" : g.Key,
                                            Value = g.Key
                                        };

            var result = values.OrderBy(i => i.Key);
            return result.ToDictionary(k => k.Key, v => v.Value);
        }

        private IDictionary<string, string> SelectValues(Func<ISkill, string> propertySelector)
        {
            var values = from item in service.Skills
                         let value = propertySelector(item)
                         let compareValue = string.IsNullOrEmpty(value) ? value : value
                         group item by compareValue
                         into g
                             select new
                                        {
                                            Key = string.IsNullOrEmpty(g.Key) ? "<None>" : g.Key,
                                            Value = g.Key
                                        };
            var additional = Enumerable.Repeat(new
                                                   {
                                                       Key = any,
                                                       Value = any
                                                   },
                                               1);

            var result = values.Concat(additional).OrderBy(i => i.Key);

            return result.ToDictionary(k => k.Key, v => v.Value);
        }

        private bool SkillFilter(object obj)
        {
            var skill = obj as ISkill;

            if(skill == null)
                return false;

            if(!skill.IsValid)
                return false;

            if(SearchForTemplateId && skill.TemplateId != SelectedTemplateId)
                return false;

            if(!IsValid(SelectedProfession.Value, skill.Profession.Name))
                return false;

            if(!IsValid(SelectedAttribute.Value, skill.Attribute.Name))
                return false;

            if(!IsValid(SelectedSkillType.Value, skill.Type))
                return false;

            if(!IsValid(SelectedSpecialType.Value, skill.SpecialType))
                return false;

            if(!IsValid(SelectedTarget.Value, skill.Target))
                return false;

            if(!IsValid(SelectedRange.Value, skill.Range))
                return false;

            if(!IsValid(SelectedProjectile.Value, skill.Projectile))
                return false;

            if(!IsValid(CausesExhaustion, skill.CausesExhaustion))
                return false;

            if(!IsValid(SelectedCampaign.Value, skill.Campaign))
                return false;

            if(!IsValid(SelectedActivationTime.Value, skill.ActivationTime.ToString()))
                return false;

            if(!IsValid(SelectedRechargeTime.Value, skill.RechargeTime.ToString()))
                return false;

            if(!IsValid(SelectedEnergyCost.Value, skill.EnergyCost.ToString()))
                return false;

            if(!IsValid(SelectedSacrificeCost.Value, skill.Sacrifice.ToString()))
                return false;

            if(!IsValid(SelectedAdrenalineCost.Value, skill.AdrenalineCost.ToString()))
                return false;

            if(!IsValid(SelectedUpkeepValue.Value, skill.UpkeepCost.ToString()))
                return false;

            if(!IsValid(Elite, skill.IsElite))
                return false;

            if(!IsValid(PvEOnly, skill.IsPvEOnly))
                return false;

            if(!IsValid(IsPvP, skill.IsPvPVersion))
                return false;

            if(!IsValid(SelectedAreaOfEffect.Value, skill.AreaOfEffect))
                return false;

            if(!IsValid(SelectedRemovesValues, skill.Removes))
                return false;

            return true;
        }

        private static bool IsValid(IEnumerable<KeyValuePair<string, string>> selectedValues, IEnumerable<string> values)
        {
            if(selectedValues.Count() == 0)
                return true;

            if(values.Count() == 0)
                return false;

            var matchingValues = from selectedValue in selectedValues
                                 join value in values on selectedValue.Value equals value.ToLower().Trim()
                                 select true;

            return matchingValues.Count() != 0;
        }

        private static bool IsValid(bool? filterValue, bool? itemValue)
        {
            // Filter is set to any
            if(filterValue.HasValue == false)
                return true;

            var item = itemValue.HasValue && itemValue.Value;
            var filter = filterValue.Value;

            return item == filter;
        }

        private static bool IsValid(string value, string itemValue)
        {
            if(value == any)
                return true;

            if(value == null || itemValue == null)
                return value == itemValue;

            return value.Equals(itemValue, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}