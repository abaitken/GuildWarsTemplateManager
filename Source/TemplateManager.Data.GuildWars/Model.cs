using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using TemplateManager.Infrastructure.Model;

namespace TemplateManager.Data.GuildWars
{
    public partial class Model
    {
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
                    var row = GetText();
                    return row.Name;
                }
            }

            public string Description
            {
                get
                {
                    var row = GetText();
                    return row.Description;
                }
            }

            public string ConciseDescription
            {
                get
                {
                    var row = GetText();
                    return row.ConciseDescription;
                }
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

            public bool IsRemoved
            {
                get { return SourceIsRemoved; }
            }

            public bool IsValid
            {
                get { return SourceIsValid; }
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

            private SkillTextRow GetText()
            {
                var result = GetSkillTextRows().FirstOrDefault();
                return result;
            }

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

            #endregion
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
                var stream = new MemoryStream();
                stream.Write(source, 0, source.Length);

                var result = new BitmapImage();
                result.BeginInit();
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();

                return result;
            }

        }
    }
}