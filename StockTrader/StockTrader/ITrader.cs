namespace stockTrader
{
    public interface ITrader
    {
        bool Buy(string symbol, double bid);
    }
}