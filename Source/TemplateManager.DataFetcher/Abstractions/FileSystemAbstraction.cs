using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TemplateManager.DataFetcher.Abstractions
{
    internal class FileSystemAbstraction : IFileSystemAbstraction
    {
        #region IFileSystemAbstraction Members

        public string Read(string path)
        {
            return File.ReadAllText(path);
        }

        public void Write(string path, string contents)
        {
            var writer = new StreamWriter(path);
            writer.Write(contents);
            writer.Close();
        }

        public Stream Open(string file, FileMode mode, FileAccess access, FileShare share)
        {
            return new FileStream(file, mode, access, share);
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public void Write(string path, Bitmap image)
        {
            image.Save(path, ImageFormat.Jpeg);
        }

        #endregion
    }
}