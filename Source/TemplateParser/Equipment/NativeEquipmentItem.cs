using System.Collections.Generic;

namespace TemplateParser.Equipment
{
    /// <summary>
    /// An item of equipment
    /// </summary>
    public struct NativeEquipmentItem
    {
        /// <summary>
        /// Slot
        /// </summary>
        public int SlotId { get; set; }

        /// <summary>
        /// Item
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Modifiers
        /// </summary>
        public IEnumerable<int> ModifierIds { get; set; }

        /// <summary>
        /// Color
        /// </summary>
        public int ColorId { get; set; }
    }
}