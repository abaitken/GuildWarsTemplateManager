using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TemplateManager.DataFetcher.Abstractions;
using TemplateManager.DataFetcher.Logging;

namespace TemplateManager.DataFetcher.CacheManagers
{
    internal class FileCacheManager : SaveOnlyFileCacheManager
    {
        public FileCacheManager(ILogger logger, IFileSystemAbstraction fileSystem, string filePath)
            : base(logger, fileSystem, filePath)
        {
        }

        protected override Dictionary<string, string> LoadCache()
        {
            if(!fileSystem.Exists(filePath))
                return base.LoadCache();

            var formatter = new BinaryFormatter();
            var stream = fileSystem.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var result = (Dictionary<string, string>) formatter.Deserialize(stream);
            stream.Close();

            return result;
        }
    }
}