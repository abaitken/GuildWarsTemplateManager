using System;

namespace TemplateParser
{
    /// <summary>
    /// Provides a method of moving through a binary string.
    /// </summary>
    public class BinaryIterator
    {
        private readonly string binaryString;
        private int currentPosition;

        /// <summary>
        /// Instantiates a BinaryIterator object
        /// </summary>
        /// <param name="value">The binary string to navigate</param>
        public BinaryIterator(string value)
        {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");

            binaryString = value;
        }

        /// <summary>
        /// Gets the length of the binary string
        /// </summary>
        internal int Length
        {
            get { return binaryString.Length; }
        }

        /// <summary>
        /// Returns the value of the next set of bits
        /// </summary>
        /// <param name="length">Number of bits to parse</param>
        /// <returns>The value</returns>
        public int GetBitSetValue(int length)
        {
            var set = GetBits(length).ToCharArray();
            Array.Reverse(set);
            return Convert.ToInt32(new String(set), 2);
        }

        private string GetBits(int length)
        {
            if(length < 1)
                throw new ArgumentOutOfRangeException("length");

            if(currentPosition + length > binaryString.Length)
            {
                currentPosition = Length;
                return "0";
            }

            var result = binaryString.Substring(currentPosition, length);
            currentPosition += length;
            return result;
        }
    }
}