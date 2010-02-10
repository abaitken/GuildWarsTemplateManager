using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TemplateManager.DataFetcher.Abstractions;
using TemplateManager.DataFetcher.Logging;

namespace TemplateManager.DataFetcher.CacheManagers
{
    internal class SaveOnlyFileCacheManager : ICacheManager
    {
        protected readonly string filePath;
        protected readonly IFileSystemAbstraction fileSystem;
        private readonly ILogger logger;
        protected Dictionary<string, string> cache;

        public SaveOnlyFileCacheManager(ILogger logger, IFileSystemAbstraction fileSystem, string filePath)
        {
            this.logger = logger;
            this.fileSystem = fileSystem;
            this.filePath = filePath;
        }

        #region ICacheManager Members

        public string Load(string id)
        {
            if(cache == null)
                cache = LoadCache();

            return cache.ContainsKey(id) ? cache[id] : null;
        }

        public void Save(string id, string data)
        {
            if(cache.ContainsKey(id))
                cache[id] = data;
            else
                cache.Add(id, data);
        }

        public void TearDown()
        {
            if(cache != null)
                SaveCache();
        }

        #endregion

        protected virtual Dictionary<string, string> LoadCache()
        {
            return new Dictionary<string, string>();
        }

        private void SaveCache()
        {
            var formatter = new BinaryFormatter();
            var stream = fileSystem.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, cache);
            stream.Close();
        }
    }
}