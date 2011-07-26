namespace TemplateParser
{
    /// <summary>
    /// Represents a set of bits
    /// </summary>
    public struct BitSet
    {
        private readonly int value;
        private readonly int bitLength;

        /// <summary>
        /// Instantiates a BitSet struct
        /// </summary>
        /// <param name="value">The value of the binary</param>
        /// <param name="bitLength">The number of bits this value should take up</param>
        public BitSet(int value, int bitLength)
        {
            this.value = value;
            this.bitLength = bitLength;
        }

        /// <summary>
        /// Value of this bit set
        /// </summary>
        public int Value
        {
            get { return value; }
        }

        /// <summary>
        /// Number of bits required
        /// </summary>
        public int BitLength
        {
            get { return bitLength; }
        }
    }
}