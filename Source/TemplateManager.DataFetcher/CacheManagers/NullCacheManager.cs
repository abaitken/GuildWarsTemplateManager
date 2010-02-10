namespace TemplateManager.DataFetcher.CacheManagers
{
    internal class NullCacheManager : ICacheManager
    {
        #region ICacheManager Members

        public string Load(string id)
        {
            return null;
        }

        public void Save(string id, string data)
        {
            // do nothing
        }

        public void TearDown()
        {
            // do nothing
        }

        #endregion
    }
}