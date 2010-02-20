using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using TemplateManager.Common;
using TemplateManager.Infrastructure.Model;
using System.Threading;
using System.Globalization;

namespace TemplateManager.Data.GuildWars
{
    public partial class Model
    {
        public const string DataFile = "TemplateManager.Data.xml";

        public static Model Load()
        {
            var result = new Model();
            result.ReadXml(DataFile);
            return result;
        }

        #region Nested type: SkillsRow

        public partial class SkillsRow : ISkill
        {
            #region ISkill Members

            public double? ActivationTime
            {
                get
                {
                    if (IsSourceActivationTimeNull())
                        return null;

                    return SourceActivationTime;
                }
            }

            public int TemplateId
            {
                get { return Id; }
            }

            public string WikiLink
            {
                get { return SourceWikiLink; }
            }

            public string Name
            {
                get
                {
                    var currentCulture = CultureInfo.CurrentUICulture.Name;
                    var row = GetSkillNameRows().FirstOrDefault(i => i.Locale == currentCulture) ??
                              GetSkillNameRows().First(i => i.Locale == "en");

                    return row.Name;
                }
            }

            public string Description
            {
                get
                {
                    var row = GetDescription();
                    return row.Description;
                }
            }

            public string ConciseDescription
            {
                get
                {
                    var row = GetDescription();
                    return row.ConciseDescription;
                }
            }

            private SkillDescriptionRow GetDescription()
            {
                var currentCulture = CultureInfo.CurrentUICulture.Name;
                return GetSkillDescriptionRows().FirstOrDefault(i => i.Locale == currentCulture) ??
                       GetSkillDescriptionRows().First(i => i.Locale == "en");
            }

            public double? RechargeTime
            {
                get { return GetSingleValue(IsSourceRechargeTimeNull, i => i.SourceRechargeTime); }
            }

            public double? EnergyCost
            {
                get { return GetSingleValue(IsSourceEnergyCostNull, i => i.SourceEnergyCost); }
            }

            public double? UpkeepCost
            {
                get { return GetSingleValue(IsSourceUpkeepCostNull, i => i.SourceUpkeepCost); }
            }

            public double? AdrenalineCost
            {
                get { return GetSingleValue(IsSourceAdrenalineCostNull, i => i.SourceAdrenalineCost); }
            }

            public double? Sacrifice
            {
                get { return GetSingleValue(IsSourceSacrificeCostNull, i => i.SourceSacrificeCost); }
            }

            public string Type
            {
                get
                {
                    return GetSkillTypesRows().First().Name;
                }
            }

            public string Campaign
            {
                get { return GetCampaignsRows().First().Name; }
            }

            public IProfession Profession
            {
                get { return GetProfessionsRows().First(); }
            }

            public IAttribute Attribute
            {
                get { return GetAttributesRows().First(); }
            }

            public string Target
            {
                get { return GetTargetsRows().First().Name; }
            }

            public string Range
            {
                get { return GetRangesRows().First().Name; }
            }

            public string SpecialType
            {
                get { return GetSpecialTypesRows().First().Name; }
            }

            public string Projectile
            {
                get { return GetProjectilesRows().First().Name; }
            }

            public string AreaOfEffect
            {
                get { return GetAreaOfEffectsRows().First().Name; }
            }

            public IEnumerable<string> Categories
            {
                get
                {
                    return from lookupRow in GetSkillsCategories_LookupRows()
                           select lookupRow.CategoriesRow.Name;
                }
            }

            public IEnumerable<string> Removes
            {
                get
                {
                    return from lookupRow in GetSkillsRemoves_LookupRows()
                           select lookupRow.RemovesRow.Name;
                }
            }

            public IEnumerable<ISkill> RelatedSkills
            {
                get
                {
                    return from lookupRow in GetRelatedSkillsRows()
                           from skillRow in lookupRow.GetSkillsRows()
                           select skillRow as ISkill;
                }
            }

            public IEnumerable<string> Causes
            {
                get
                {
                    return from lookupRow in GetSkillsCauses_LookupRows()
                           select lookupRow.CausesRow.Name;
                }
            }

            public BitmapImage Image
            {
                get { return GetImagesRows().First().GetImage(); }
            }

            public bool? IsElite
            {
                get { return GetSingleValue(IsSourceIsEliteNull, i => i.SourceIsElite); }
            }

            public bool? CausesExhaustion
            {
                get { return GetSingleValue(IsSourceCausesExhaustionNull, i => i.SourceCausesExhaustion); }
            }

            public bool? IsPvEOnly
            {
                get { return GetSingleValue(IsSourceIsPvEOnlyNull, i => i.SourceIsPvEOnly); }
            }

            public bool? HasPvP
            {
                get { return GetSingleValue(IsSourceHasPvPNull, i => i.SourceHasPvP); }
            }

            public bool? IsPvPVersion
            {
                get { return GetSingleValue(IsSourceIsPvPNull, i => i.SourceIsPvP); }
            }

            #endregion

            private T? GetSingleValue<T>(Func<bool> isNull, Func<SkillsRow, T> getter) where T : struct
            {
                if (isNull())
                    return null;

                return getter(this);
            }
        }

        #endregion

        public partial class ProfessionsRow : IProfession
        {
            #region IProfession Members

            public bool IsValid
            {
                get { return !IsTemplateIdNull(); }
            }

            public BitmapImage Image
            {
                get { return GetImagesRows().First().GetImage(); }
            }

            #endregion

            public bool Equals(IProfession other)
            {
                return other.Name == Name;
            }
        }

        public partial class AttributesRow : IAttribute
        {
            #region IAttribute Members

            public bool IsValid
            {
                get { return !IsTemplateIdNull(); }
            }

            #endregion
        }

        public partial class ImagesRow
        {
            private BitmapImage image;

            public BitmapImage GetImage()
            {
                if (image == null)
                    image = GetImage(Data);

                return image;
            }

            private static BitmapImage GetImage(byte[] source)
            {
                return ImageSerializer.CreateImage(source);
            }

        }
    }
}