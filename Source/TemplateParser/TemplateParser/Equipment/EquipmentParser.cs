using System.Collections.Generic;
using System.Linq;

namespace TemplateParser.Equipment
{
    /// <summary>
    /// Parses and generates equipment templates
    /// </summary>
    public class EquipmentParser : TemplateParserBase<NativeEquipmentBuild>
    {
        #region constants

        private const int DefaultItemIdBitLength = 9;
        private const int DefaultModifierIdBitLength = 9;
        private const int EquipmentSlotBitLength = 3;
        private const int ExpectedTemplateType = 15;
        private const int ExpectedVersion = 0;
        private const int HeaderBitLength = 4;
        private const int ItemColorBitLength = 4;

        private const int ItemCountBitLength = 3;
        private const int ItemIdBitLength = 4;
        private const int LegacyVersion = 1;
        private const int ModifierCountBitLength = 2;
        private const int ModifierIdBitLength = 4;
        private const int VersionBitLength = 4;

        #endregion

        /// <summary>
        /// Get a collection of all the values required in the template
        /// </summary>
        /// <param name="source">The object that contains all the data</param>
        /// <returns>List of the bit sets</returns>
        protected override IEnumerable<BitSet> GetValues(NativeEquipmentBuild source)
        {
            yield return new BitSet(ExpectedTemplateType, HeaderBitLength);
            yield return new BitSet(ExpectedVersion, VersionBitLength);
            yield return new BitSet(DefaultItemIdBitLength, ItemIdBitLength);
            yield return new BitSet(DefaultModifierIdBitLength, ModifierIdBitLength);
            yield return new BitSet(source.Count(), ItemCountBitLength);

            foreach(var item in source)
            {
                yield return new BitSet(item.SlotId, EquipmentSlotBitLength);
                yield return new BitSet(item.ItemId, DefaultItemIdBitLength);
                yield return new BitSet(item.ModifierIds.Count(), ModifierCountBitLength);
                yield return new BitSet(item.ColorId, ItemCountBitLength);

                foreach(var modifier in item.ModifierIds)
                    yield return new BitSet(modifier, DefaultModifierIdBitLength);
            }
        }

        /// <summary>
        /// Implementation of parsing the binary extracted from the template code
        /// </summary>
        /// <param name="binaryIterator">The binary iterator to help navigatee the binary string</param>
        /// <returns>The resulting object with data from the template</returns>
        protected override NativeEquipmentBuild ParseBinary(BinaryIterator binaryIterator)
        {
            var header = binaryIterator.GetBitSetValue(HeaderBitLength);

            if(header != LegacyVersion)
            {
                if(header != ExpectedTemplateType)
                    return null;

                var version = binaryIterator.GetBitSetValue(VersionBitLength);

                if(version != ExpectedVersion)
                    return null;
            }

            var bitsPerItemId = binaryIterator.GetBitSetValue(ItemIdBitLength);
            var bitsPerModifierId = binaryIterator.GetBitSetValue(ModifierIdBitLength);

            var itemCount = binaryIterator.GetBitSetValue(ItemCountBitLength);

            var result = new NativeEquipmentBuild();

            for(var i = 0; i < itemCount; i++)
            {
                var item = new NativeEquipmentItem
                               {
                                   SlotId = binaryIterator.GetBitSetValue(EquipmentSlotBitLength),
                                   ItemId = binaryIterator.GetBitSetValue(bitsPerItemId)
                               };

                var modifierCount = binaryIterator.GetBitSetValue(ModifierCountBitLength);
                item.ColorId = binaryIterator.GetBitSetValue(ItemColorBitLength);

                var modifiers = new List<int>();

                for(var m = 0; m < modifierCount; m++)
                    modifiers.Add(binaryIterator.GetBitSetValue(bitsPerModifierId));

                item.ModifierIds = modifiers;

                result.Add(item);
            }

            return result;
        }

        protected override bool CanParseTemplateCodeImpl(BinaryIterator binaryIterator)
        {
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
    }
}