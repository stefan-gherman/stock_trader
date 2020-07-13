using stock_trader_app_DI_csharp.StockTrader;

namespace stockTrader
{
    public class Trader : ITrader
    {
        private static Trader _instance;
        private  static IAPIService _stockApiService;
        

        public static Trader Instance()
        {
        
                if (_instance == null)
                {
                    var stockApiService = StockAPIService.Instance();
                    _instance = new Trader(stockApiService);
                }
                return _instance;
        }

        public IAPIService Service
        {
            get { return _stockApiService; }
            set { _stockApiService = value; }
        }

        public IAPIService GetService()
        {
            return _stockApiService;
        }

        private Trader (IAPIService stockApiService)
        {
            _stockApiService = stockApiService;
        }
        
        /// <summary>
        /// Checks the price of a stock, and buys it if the price is not greater than the bid amount.
        /// </summary>
        /// <param name="symbol">the symbol to buy, e.g. aapl</param>
        /// <param name="bid">the bid amount</param>
        /// <returns>whether any stock was bought</returns>
        public bool Buy(string symbol, double bid) 
        {
            double price = _stockApiService.GetPrice(symbol);
            bool result;
            if (price <= bid) {
                result = true;
                _stockApiService.Buy(symbol);
                //Logger.Instance.Log("Purchased " + symbol + " stock at $" + bid + ", since its higher that the current price ($" + price + ")");
            }
            else {
                //Logger.Instance.Log("Bid for " + symbol + " was $" + bid + " but the stock price is $" + price + ", no purchase was made.");
                result = false;
            }
            return result;
    }
        
    }
}