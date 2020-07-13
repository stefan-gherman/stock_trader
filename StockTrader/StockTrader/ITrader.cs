using stock_trader_app_DI_csharp.StockTrader;

namespace stockTrader
{
    public interface ITrader
    {
        bool Buy(string symbol, double bid);
        IAPIService GetService();
    }
}