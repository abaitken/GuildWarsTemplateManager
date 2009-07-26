using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace TemplateManager.BitmapEncoding
{
    abstract class BitmapEncoderBase : IBitmapEncoder
    {
        protected abstract IList<BitmapFrame> Frames { get; }
        protected abstract void Save(MemoryStream stream);

        public MemoryStream GetImageSource(BitmapSource source)
        {
            var memStream = new MemoryStream();
            Frames.Add(BitmapFrame.Create(source));
            Save(memStream);
            return memStream;
        }
    }
}