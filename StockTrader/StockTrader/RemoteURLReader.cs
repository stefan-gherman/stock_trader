using stock_trader_app_DI_csharp.StockTrader;
using System.Net;

namespace stockTrader
{
    public class RemoteURLReader : IURLReader
    {
        private static RemoteURLReader _instance;

        public static RemoteURLReader Instance ()
        {
            if ( _instance == null)
            {
                _instance = new RemoteURLReader();
            }

            return _instance;
        }
        private RemoteURLReader()
        {

        }
        public string ReadFromUrl(string endpoint) {
            using(var client = new WebClient()) {
                return client.DownloadString(endpoint);
            }
        }
    }
}