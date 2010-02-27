using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateParser
{
    internal static class StringExtensions
    {
        public static string Reverse(this string source)
        {
            var result = source.ToCharArray();
            Array.Reverse(result);
            return new string(result);
        }

        public static IEnumerable<string> ChunkString(this string source, int chunkSize)
        {
            for(var i = 0; i < source.Length; i += chunkSize)
            {
                if(source.Length - i < chunkSize)
                    chunkSize = source.Length - i;

                yield return source.Substring(i, chunkSize);
            }
        }

        public static string Combine<T>(this IEnumerable<T> source)
        {
            var builder = new StringBuilder();

            foreach(var item in source)
                builder.Append(item);

            return builder.ToString();
        }
    }
}