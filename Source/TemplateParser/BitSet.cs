namespace TemplateParser
{
    /// <summary>
    /// Represents a set of bits
    /// </summary>
    public class BitSet
    {
        /// <summary>
        /// Instantiates a BitSet struct
        /// </summary>
        /// <param name="value">The value of the binary</param>
        /// <param name="bitLength">The number of bits this value should take up</param>
        public BitSet(int value, int bitLength)
        {
            Value = value;
            BitLength = bitLength;
        }

        /// <summary>
        /// Value of this bit set
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Number of bits required
        /// </summary>
        public int BitLength { get; set; }
    }
}