using System;

namespace stockTrader
{

  internal class TradingApp
  {
		private ITrader _trader;
    public static void Main(string[] args)
    {
			var trader = Trader.Instance();
	    TradingApp app = new TradingApp(trader);
	    app.Start(trader);
    }

	public TradingApp(ITrader trader)
        {
			_trader = trader;
        }

    public void Start(ITrader trader)
    {
	    Console.WriteLine("Enter a stock symbol (for example aapl):");
	    string symbol = Console.ReadLine();
	    Console.WriteLine("Enter the maximum price you are willing to pay: ");
	    double price;
	    while (!double.TryParse(Console.ReadLine(), out price))
	    {
		    Console.WriteLine("Please enter a number.");
	    }
	    
	    try {
		    bool purchased = trader.Buy(symbol, price);
		    if (purchased) {
			    Logger.Instance.Log("Purchased stock!");
		    }
		    else {
			    Logger.Instance.Log("Couldn't buy the stock at that price.");
		    }
	    } catch (Exception e) {
		    Logger.Instance.Log("There was an error while attempting to buy the stock: " + e.Message);
	    }
        Console.ReadLine();
    }
  }
}