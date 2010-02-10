using System.Drawing;
using TemplateManager.DataFetcher.CacheManagers;
using TemplateManager.DataFetcher.Logging;

namespace TemplateManager.DataFetcher.DataProviders
{
    internal class CacheOnlyDataProvider : DataProviderBase
    {
        protected readonly ICacheManager cacheManager;
        protected readonly ILogger logger;

        public CacheOnlyDataProvider(ILogger logger, string domain, ICacheManager cacheManager)
            : base(domain)
        {
            this.logger = logger;
            this.cacheManager = cacheManager;
        }

        public override string RequestData(string articleTitle, bool raw)
        {
            var url = CreateUrl(articleTitle, raw);
            return Request(url);
        }

        private string Request(string url)
        {
            var cacheResult = cacheManager.Load(url);

            if(cacheResult != null)
            {
                logger.Log(GetType(), string.Format("Loading from cache: {0}", url), LogSeverity.InformationLow);
                return cacheResult;
            }

            return RequestLiveData(url);
        }

        protected virtual string RequestLiveData(string url)
        {
            logger.Log(GetType(), string.Format("Failed to load from cache: {0}", url), LogSeverity.InformationHigh);
            return string.Empty;
        }

        public override Bitmap RequestImage(string imageAddress)
        {
            return null;
        }
    }
}