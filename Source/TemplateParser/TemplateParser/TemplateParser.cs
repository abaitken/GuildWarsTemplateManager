using System.Collections.Generic;

namespace TemplateParser
{
    /// <summary>
    /// Base class for the template parser. Provides methods to help converting the Guild Wars template codes
    /// </summary>
    /// <typeparam name="T">Type used to represent the template data. 
    /// The subclass will be required to parse a template code and return this type or parse this type and provide a template code</typeparam>
    public abstract class TemplateParserBase<T>
    {
        #region encode

        /// <summary>
        /// Create the template code for the given object
        /// </summary>
        /// <param name="source">The object data</param>
        /// <returns>The template code</returns>
        public string CreateTemplateCode(T source)
        {
            Utility.ThrowIfNull(source, "source");

            return Base64BinaryConverter.ConvertBitSetsToBase64(GetValues(source));
        }

        /// <summary>
        /// Get a collection of all the values required in the template
        /// </summary>
        /// <param name="source">The object that contains all the data</param>
        /// <returns>List of the bit sets</returns>
        protected abstract IEnumerable<BitSet> GetValues(T source);

        #endregion

        #region decode

        /// <summary>
        /// Parse a template code and create a object from it
        /// </summary>
        /// <param name="templateCode">Template code to parse</param>
        /// <returns>The resulting object with data from the template</returns>
        public T ParseTemplateCode(string templateCode)
        {
            if (!Base64BinaryConverter.ValidateBase64String(templateCode))
                return default(T);

            string binaryString = Base64BinaryConverter.GetBinaryString(templateCode);
            return ParseBinary(new BinaryIterator(binaryString));
        }

        /// <summary>
        /// Implementation of parsing the binary extracted from the template code
        /// </summary>
        /// <param name="binaryIterator">The binary iterator to help navigatee the binary string</param>
        /// <returns>The resulting object with data from the template</returns>
        protected abstract T ParseBinary(BinaryIterator binaryIterator);

        #endregion
    }
}