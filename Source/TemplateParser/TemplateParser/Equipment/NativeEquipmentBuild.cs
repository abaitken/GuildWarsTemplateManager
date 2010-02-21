using System;
using System.Collections;
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

        public string TemplateCode { get; set; }

        #region IEnumerable<NativeEquipmentItem> Members

        public IEnumerator<NativeEquipmentItem> GetEnumerator()
        {
            return inner.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return inner.GetEnumerator();
        }

        #endregion

        public void Add(NativeEquipmentItem item)
        {
            inner.Add(item);
        }
    }
}