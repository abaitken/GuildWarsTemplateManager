using System.Drawing;

namespace TemplateManager.DataFetcher.DataProviders
{
    internal abstract class DataProviderBase : IDataProvider
    {
        private readonly string domain;

        protected DataProviderBase(string domain)
        {
            this.domain = domain;
        }

        #region IDataProvider Members

        public abstract string RequestData(string articleTitle, bool raw);

        public abstract Bitmap RequestImage(string imageAddress);

        public string CreateUrl(string articleTitle, bool raw)
        {
            var requestPart = raw ? "/index.php?title={0}&action=raw" : "/index.php?title={0}";
            return string.Format("http://{0}{1}", domain, string.Format(requestPart, articleTitle));
        }

        #endregion

        protected string CreateImageUrl(string imageUrl)
        {
            return string.Format("http://{0}{1}", domain, imageUrl);
        }
    }
}