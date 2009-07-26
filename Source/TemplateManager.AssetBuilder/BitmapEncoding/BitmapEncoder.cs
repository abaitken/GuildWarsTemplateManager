using System;

namespace TemplateManager.BitmapEncoding
{
    internal static class BitmapEncoder
    {
        public static IBitmapEncoder CreateBitmapEncoder(EncoderType type)
        {
            switch(type)
            {
                case EncoderType.Jpeg:
                    return new JpegBitmapEncoderImpl();
                case EncoderType.Png:
                    return new PngBitmapEncoderImpl();
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}