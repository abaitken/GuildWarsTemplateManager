using System;
using System.Diagnostics;

namespace TemplateParser
{
    internal static class Utility
    {
        public static void ThrowIfNull<T>(T source, string paramName)
        {
            Debug.Assert(source != null,
                         "source != null");

            if (source == null)
                throw new ArgumentNullException(paramName);
        }

        public static void ThrowIfNullOrEmpty(string source, string paramName)
        {
            Debug.Assert(!string.IsNullOrEmpty(source),
                         "!string.IsNullOrEmpty(source)");

            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(paramName);
        }
    }
}