using System.Collections.Generic;

namespace TemplateManager.Common
{
    public class HashCodeBuilder
    {
        private const int initialPrime = 7;
        private const int primeMultiplier = 31;
        private int calculatedHash;

        public HashCodeBuilder()
        {
            calculatedHash = initialPrime;
        }

        public void Append(object value)
        {
            calculatedHash = AppendToHash(calculatedHash, value);
        }

        public static int Build(IEnumerable<int> items)
        {
            var builder = new HashCodeBuilder();

            foreach (var item in items)
                builder.Append(item);

            return builder.ToInt32();
        }

        public int ToInt32()
        {
            return calculatedHash;
        }

        private static int AppendToHash(int hash, int valueToAppend)
        {
            return hash * primeMultiplier + valueToAppend;
        }

        private static int AppendToHash(int hash, object obj)
        {
            return AppendToHash(hash, obj == null ? 0 : obj.GetHashCode());
        }
    }
}
