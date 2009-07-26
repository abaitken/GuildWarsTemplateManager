using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace TemplateManager.BitmapEncoding
{
    internal class PngBitmapEncoderImpl : BitmapEncoderBase
    {
        private readonly PngBitmapEncoder encoder = new PngBitmapEncoder();

        protected override IList<BitmapFrame> Frames
        {
            get { return encoder.Frames; }
        }

        protected override void Save(MemoryStream stream)
        {
            encoder.Save(stream);
        }
    }
}