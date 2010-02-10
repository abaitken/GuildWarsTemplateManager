using System.Drawing;

namespace TemplateManager.DataFetcher.Abstractions
{
    public interface INetworkAbstraction
    {
        string Fetch(string url);
        Bitmap FetchImage(string url);
    }
}