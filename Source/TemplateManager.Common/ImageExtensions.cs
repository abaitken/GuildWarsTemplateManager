using System;
using System.Windows.Media.Imaging;

namespace TemplateManager.Common
{
    public static class ImageExtensions
    {
        public static BitmapSource CreateSource(this Uri source)
        {
            var decoder = BitmapDecoder.Create(source,
                                               BitmapCreateOptions.PreservePixelFormat,
                                               BitmapCacheOption.Default);

            return decoder.Frames[0];
        }
    }
}
