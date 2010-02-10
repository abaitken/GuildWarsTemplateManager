using System.Drawing;
using System.IO;
using System.Net;
using TemplateManager.DataFetcher.Logging;

namespace TemplateManager.DataFetcher.Abstractions
{
    internal class NetworkAbstraction : INetworkAbstraction
    {
        private readonly WebClient client = new WebClient();
        private readonly ILogger logger;

        public NetworkAbstraction(ILogger logger)
        {
            this.logger = logger;
        }

        #region INetworkAbstraction Members

        public string Fetch(string url)
        {
            var request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            ((HttpWebRequest) request).UserAgent = "Guild Wars Data Fetcher";
            request.Method = "GET";

            WebResponse response;

            try
            {
                response = request.GetResponse();
            }
            catch(WebException ex)
            {
                logger.Log(GetType(), string.Format("Failed to get url. {0}", ex.Message), LogSeverity.Error);
                return string.Empty;
            }

            var reader = new StreamReader(response.GetResponseStream());

            var result = reader.ReadToEnd();

            reader.Close();
            response.Close();
            return result;
        }

        public Bitmap FetchImage(string url)
        {
            var stream = client.OpenRead(url);

            var result = new Bitmap(stream);
            stream.Flush();
            stream.Close();

            return result;
        }

        #endregion
    }
}