using System;
using System.Threading;
using System.Xml.Linq;

namespace TemplateManager.Infrastructure.Extensions
{
    public static class LinqToXmlExtensions
    {
        public static string GetValueOrDefault(this XElement source)
        {
            if (source == null)
                return string.Empty;

            return source.Value;
        }

        public static int ParseIntOrDefault(this XElement source)
        {
            return source.ParseIntOrDefault(Thread.CurrentThread.CurrentCulture);
        }

        public static int ParseIntOrDefault(this XElement source, IFormatProvider provider)
        {
            if (source == null)
                return default(int);

            return source.ParseInt(provider);
        }

        public static int ParseInt(this XElement source)
        {
            return source.ParseInt(Thread.CurrentThread.CurrentCulture);
        }

        public static int ParseInt(this XElement source, IFormatProvider provider)
        {
            return int.Parse(source.Value, provider);
        }

        public static double ParseDoubleOrDefault(this XElement source)
        {
            return source.ParseDoubleOrDefault(Thread.CurrentThread.CurrentCulture);
        }

        public static double ParseDoubleOrDefault(this XElement source, IFormatProvider provider)
        {
            if (source == null)
                return default(double);

            return double.Parse(source.Value, provider);
        }

        public static bool ParseBoolOrDefault(this XElement source)
        {
            if (source == null)
                return default(bool);

            return source.ParseBool();
        }

        public static bool ParseBool(this XElement source)
        {
            if (source == null)
                return default(bool);

            return bool.Parse(source.Value);
        }
    }
}