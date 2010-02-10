namespace TemplateManager.DataFetcher.CacheManagers
{
    public interface ICacheManager
    {
        string Load(string id);
        void Save(string id, string data);
        void TearDown();
    }
}