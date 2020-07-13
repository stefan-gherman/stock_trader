using System;
using Newtonsoft.Json.Linq;
using stock_trader_app_DI_csharp.StockTrader;

namespace stockTrader
{
    /// <summary>
    /// Stock price service that gets prices from a remote API
    /// </summary>
    public class StockAPIService : IAPIService {

        private string _apiPath /*= "https://financialmodelingprep.com/api/v3/stock/real-time-price/{0}"*/;
        private static IURLReader _reader;

        private static StockAPIService _instance;

        public static StockAPIService Instance()
        {
           
                if (_instance == null)
                {
                string apiPath = "https://financialmodelingprep.com/api/v3/stock/real-time-price/{0}";
                var reader = RemoteURLReader.Instance();
                    _instance = new StockAPIService(apiPath, reader);
                }

                return _instance;
        }
        private StockAPIService(string apiPath, IURLReader reader)
        {
            _apiPath = apiPath;
            _reader = reader;
        }
        /// <summary>
        /// Get stock price from iex
        /// </summary>
        /// <param name="symbol">symbol Stock symbol, for example "aapl"</param>
        /// <returns>the stock price</returns>
        public double GetPrice(string symbol) {
            string url = String.Format(_apiPath, symbol);
            string result = _reader.ReadFromUrl(url);
            var json = JObject.Parse(result);
            string price = json.GetValue("price").ToString();
            return double.Parse(price);
    }
	
    /// <summary>
    /// Buys a share of the given stock at the current price. Returns false if the purchase fails 
    /// </summary>
    public bool Buy(string symbol) {
        // Stub. No need to implement this.
        return true;
    }
}
}