using System.Collections.Generic;

namespace TemplateParser.Equipment
{
    public class NativeEquipmentBuild : IEnumerable<NativeEquipmentItem>
    {
        private readonly IList<NativeEquipmentItem> inner;

        public NativeEquipmentBuild()
        {
            inner = new List<NativeEquipmentItem>();
        }

        public void Add(NativeEquipmentItem item)
        {
            inner.Add(item);
        }

        #region IEnumerable<NativeEquipmentItem> Members

        public IEnumerator<NativeEquipmentItem> GetEnumerator()
        {
            return inner.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return inner.GetEnumerator();
        }

        #endregion
    }
}
