using System.IO;
using System.Windows.Media.Imaging;

namespace TemplateManager.Common
{
    public class ImageSerializer
    {
        public static BitmapImage CreateImage(byte[] source)
        {
            var stream = new MemoryStream(source);

            var result = new BitmapImage();
            result.BeginInit();
            result.StreamSource = stream;
            result.EndInit();
            result.Freeze();

            return result;
        }

        public static byte[] GetImage(string imageName)
        {
            var reader = new StreamReader(imageName);
            var stream = reader.BaseStream;
            byte[] buffer = null;
            if(stream != null && stream.Length > 0)
            {
                using(var br = new BinaryReader(stream))
                {
                    buffer = br.ReadBytes((int) stream.Length);
                }
            }

            reader.Close();

            return buffer;
        }
    }
}