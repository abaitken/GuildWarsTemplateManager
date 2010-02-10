using System.Drawing;
using System.IO;

namespace TemplateManager.DataFetcher.Abstractions
{
    public interface IFileSystemAbstraction
    {
        string Read(string path);
        void Write(string path, string contents);
        Stream Open(string file, FileMode mode, FileAccess access, FileShare share);
        bool Exists(string path);

        void Write(string path, Bitmap image);
    }
}