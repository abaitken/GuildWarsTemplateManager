using System;
using System.Collections.Generic;
using System.Linq;

namespace TemplateParser.Skills
{
    /// <summary>
    /// Parses a skill template code
    /// </summary>
    public class SkillBuildParser : TemplateParserBase<NativeSkillBuild>
    {
        #region constants

        private const int AttributeBitLength = DefaultAttributeBitLength + AttributeBitPadding;
        private const int AttributeBitPadding = 4;
        private const int AttributeCountBitLength = 4;

        private const int AttributeSpaceBitLength = 4;

        private const int AttributeValueBitLength = 4;
        private const int DefaultAttributeBitLength = 1;
        private const int DefaultProfessionBitLength = 0;

        private const int DefaultSkillBitLength = 3;

        private const int EndPaddingBitLength = 11;
        private const int EndPaddingValue = 0;
        private const int ExpectedTemplateType = 14;
        private const int ExpectedVersion = 0;
        private const int HeaderBitLength = 4;
        private const int LegacyVersion = 0;


        // Maximum possible number of bits a template code could take up
        private const int MaxBitLength = HeaderBitLength +
                                         VersionBitLength +
                                         ProfessionSpaceBitLength +
                                         ProfessionBitLength +
                                         ProfessionBitLength +
                                         AttributeCountBitLength +
                                         AttributeSpaceBitLength +
                                         (AttributeBitLength * 8) + (AttributeValueBitLength * 8) +
                                         SkillSpaceBitLength +
                                         (SkillBitLength * 8) +
                                         EndPaddingBitLength;

        private const int ProfessionBitLength = (DefaultProfessionBitLength * 2) + ProfessionBitPadding;
        private const int ProfessionBitPadding = 4;
        private const int ProfessionSpaceBitLength = 2;
        private const int SkillBitLength = DefaultSkillBitLength + SkillBitPadding;
        private const int SkillBitPadding = 8;
        private const int SkillCount = 8;
        private const int SkillSpaceBitLength = 4;
        private const int VersionBitLength = 4;

        #endregion

        /// <summary>
        /// Implementation of parsing the binary extracted from the template code
        /// </summary>
        /// <param name="binaryIterator">The binary iterator to help navigatee the binary string</param>
        /// <returns>The resulting object with data from the template</returns>
        protected override NativeSkillBuild ParseBinary(BinaryIterator binaryIterator)
        {
            if(binaryIterator.Length > MaxBitLength)
                return null;

            // Get header
            var header = binaryIterator.GetBitSetValue(HeaderBitLength);

            if(header != LegacyVersion)
            {
                if(header != ExpectedTemplateType)
                    return null;

                var version = binaryIterator.GetBitSetValue(VersionBitLength);

                if(version != ExpectedVersion)
                    return null;
            }

            var result = new NativeSkillBuild();

            // Get professions
            var professionBitSpace = binaryIterator.GetBitSetValue(ProfessionSpaceBitLength);
            professionBitSpace = (professionBitSpace * 2) + ProfessionBitPadding;

            result.PrimaryProfessionId = binaryIterator.GetBitSetValue(professionBitSpace);
            result.SecondaryProfessionId = binaryIterator.GetBitSetValue(professionBitSpace);

            // Get attributes
            var attributeCount = binaryIterator.GetBitSetValue(AttributeCountBitLength);
            var attributeBitSpace = binaryIterator.GetBitSetValue(AttributeSpaceBitLength) + AttributeBitPadding;

            for(var index = 0; index != attributeCount; index++)
            {
                var attributeId = binaryIterator.GetBitSetValue(attributeBitSpace);
                var attributeValue = binaryIterator.GetBitSetValue(AttributeValueBitLength);

                result.AddAttribute(attributeId, attributeValue);
            }

            // Get skills
            var skillBitSpace = binaryIterator.GetBitSetValue(SkillSpaceBitLength) + SkillBitPadding;

            for(var index = 0; index != SkillCount; index++)
            {
                var skillId = binaryIterator.GetBitSetValue(skillBitSpace);

                result.AddSkill(skillId);
            }

            return result;
        }

        protected override bool CanParseTemplateCodeImpl(BinaryIterator binaryIterator)
        {
            if(binaryIterator.Length > MaxBitLength)
                return false;

            // Get header
            var header = binaryIterator.GetBitSetValue(HeaderBitLength);

            if(header == LegacyVersion)
                return false;

            if(header != ExpectedTemplateType)
                return false;

            var version = binaryIterator.GetBitSetValue(VersionBitLength);

            if(version != ExpectedVersion)
                return false;

            return true;
        }

        /// <summary>
        /// Get a collection of all the values required in the template
        /// </summary>
        /// <param name="source">The object that contains all the data</param>
        /// <returns>List of the bit sets</returns>
        protected override IEnumerable<BitSet> GetValues(NativeSkillBuild source)
        {
            if(source.SkillIds.Count() != SkillCount)
                throw new InvalidOperationException("Invalid skill count");

            if(source.PrimaryProfessionId == source.SecondaryProfessionId)
                throw new InvalidOperationException("Cannot have matching primary and secondary professions");

            if(source.PrimaryProfessionId == 0)
                throw new InvalidOperationException("Must have a primary profession");

            yield return new BitSet(ExpectedTemplateType, HeaderBitLength);
            yield return new BitSet(ExpectedVersion, VersionBitLength);
            yield return new BitSet(DefaultProfessionBitLength, ProfessionSpaceBitLength);
            yield return new BitSet(source.PrimaryProfessionId, ProfessionBitLength);
            yield return new BitSet(source.SecondaryProfessionId, ProfessionBitLength);
            yield return new BitSet(source.AttributeIdValuePairs.Count(), AttributeCountBitLength);
            yield return new BitSet(DefaultAttributeBitLength, AttributeSpaceBitLength);

            foreach(var attribute in source.AttributeIdValuePairs)
            {
                yield return new BitSet(attribute.Key, AttributeBitLength);
                yield return new BitSet(attribute.Value, AttributeValueBitLength);
            }

            yield return new BitSet(DefaultSkillBitLength, SkillSpaceBitLength);

            foreach(var skill in source.SkillIds)
                yield return new BitSet(skill, SkillBitLength);

            yield return new BitSet(EndPaddingValue, EndPaddingBitLength);
        }
    }
}