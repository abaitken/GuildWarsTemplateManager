using System.Drawing;

namespace TemplateManager.DataFetcher.DataProviders
{
    public interface IDataProvider
    {
        string RequestData(string articleTitle, bool raw);
        Bitmap RequestImage(string imageAddress);
        string CreateUrl(string articleTitle, bool raw);
    }
}