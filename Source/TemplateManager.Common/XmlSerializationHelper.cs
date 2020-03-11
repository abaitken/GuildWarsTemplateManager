using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace TemplateManager.Common
{
    public static class XmlSerializationHelper
    {
        public static T LoadFromString<T>(string document)
        {

            var bytes = document.ToCharArray().Select(i => Convert.ToByte(i)).ToArray();
            var reader = new MemoryStream(bytes);

            var result = Deserialize<T>(reader);
            reader.Close();

            return result;
        }

        private static T Deserialize<T>(Stream stream)
        {
            var xmlReader = XmlReader.Create(stream);
            var s = new XmlSerializer(typeof(T));
            if (!s.CanDeserialize(xmlReader))
                throw new InvalidOperationException("Cannot deserialize given string");

            var obj = s.Deserialize(xmlReader);
            xmlReader.Close();

            var result = (T)obj;

            return result;
        }

        public static T LoadFromFile<T>(string path)
        {
            var reader = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            var result = Deserialize<T>(reader);
            reader.Close();

            return result;
        }

        public static void SaveToFile<T>(T subject, string path)
        {
            var writer = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);

            Serialize(subject, writer);
            writer.Close();
        }

        private static void Serialize<T>(T subject, Stream stream)
        {
            var xmlWriter = XmlWriter.Create(stream);
            var s = new XmlSerializer(typeof(T));

            s.Serialize(xmlWriter, subject);
            xmlWriter.Close();
        }
    }
}
