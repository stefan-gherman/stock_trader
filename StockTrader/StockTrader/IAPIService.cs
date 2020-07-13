using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_trader_app_DI_csharp.StockTrader
{
    public interface IAPIService
    {
        double GetPrice(string symbol);
        bool Buy(string symbol);
        IURLReader GetReader();
        string GetAPIPath();
    }
}
