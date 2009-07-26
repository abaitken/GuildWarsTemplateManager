using System.IO;
using System.Windows.Media.Imaging;

namespace TemplateManager.BitmapEncoding
{
    internal interface IBitmapEncoder
    {
        MemoryStream GetImageSource(BitmapSource source);
    }
}