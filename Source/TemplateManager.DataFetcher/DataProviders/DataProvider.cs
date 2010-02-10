using System.Drawing;
using TemplateManager.DataFetcher.Abstractions;
using TemplateManager.DataFetcher.CacheManagers;
using TemplateManager.DataFetcher.Logging;

namespace TemplateManager.DataFetcher.DataProviders
{
    internal class DataProvider : CacheOnlyDataProvider
    {
        private readonly INetworkAbstraction networkInterface;

        public DataProvider(ILogger logger,
                            string domain,
                            ICacheManager cacheManager,
                            INetworkAbstraction networkInterface)
            : base(logger, domain, cacheManager)
        {
            this.networkInterface = networkInterface;
        }

        protected override string RequestLiveData(string url)
        {
            logger.Log(GetType(), string.Format("Requesting: {0}", url), LogSeverity.InformationHigh);

            var result = networkInterface.Fetch(url);
            cacheManager.Save(url, result);

            return result;
        }

        public override Bitmap RequestImage(string imageAddress)
        {
            var address = CreateImageUrl(imageAddress);

            return networkInterface.FetchImage(address);
        }
    }
}