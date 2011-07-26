using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TemplateParser
{
    /// <summary>
    /// Utility class that converts between base64 strings and binary
    /// </summary>
    internal static class Base64BinaryConverter
    {
        private const string Table = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

        #region encoding

        /// <summary>
        /// Converts a list of bit sets to a Base64 string
        /// </summary>
        /// <param name="bitSets">The list of bit sets</param>
        /// <returns>The resulting base64 string</returns>
        public static string ConvertBitSetsToBase64(IEnumerable<BitSet> bitSets)
        {
            var bits = from set in bitSets
                       select IntegerToBinaryString(set.Value, set.BitLength);

            var binaryString = new StringBuilder();
            foreach(var item in bits)
                binaryString.Append(item);

            return BinaryStringToBase64(binaryString.ToString());
        }

        private static string BinaryStringToBase64(string binaryString)
        {
            var values = from item in binaryString.ChunkString(6)
                         select BinaryStringToInteger(item);

            var result = from item in values
                         select Table.Substring(item, 1);

            return result.Combine();
        }

        private static int BinaryStringToInteger(string chunk)
        {
            var result = 0;

            var temp = chunk;

            for(var i = 0; i < temp.Length; i++)
                result += int.Parse(temp.Substring(i, 1), CultureInfo.InvariantCulture) * (1 << i);

            return result;
        }

        private static string IntegerToBinaryString(int value, int length)
        {
            // TODO : What if the length is less than the over length of the resulting string
            return ToBinary(value).PadLeft(length, '0').Reverse();
        }

        private static string ToBinary(int value)
        {
            var result = "";

            while(value > 0)
            {
                var binaryHolder = value % 2;
                result += binaryHolder;
                value = value / 2;
            }

            // The algoritm gives us the binary number in reverse order (mirrored)
            // We store it in an array so that we can reverse it back to normal
            return result.Reverse();
        }

        #endregion

        #region decoding

        /// <summary>
        /// Validates a base64 string
        /// </summary>
        /// <param name="itemToValidate">The base64 string to validate</param>
        /// <returns></returns>
        public static bool ValidateBase64String(string itemToValidate)
        {
            foreach(var character in itemToValidate.ToCharArray())
                if(!Table.Contains(character))
                    return false;

            return true;
        }

        /// <summary>
        /// Converts a base64 string into a binary string
        /// </summary>
        /// <param name="value">The base64 string to convert</param>
        /// <returns>The resulting binary string</returns>
        public static string GetBinaryString(string value)
        {
            var result = from item in value.ToCharArray()
                         select BinaryValueOfChar(item);

            return result.Combine();
        }

        private static string BinaryValueOfChar(char item)
        {
            return Convert.ToString(Table.IndexOf(item), 2).PadLeft(6, '0').Reverse();
        }

        #endregion
    }
}