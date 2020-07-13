using System;
using NSubstitute;
using NUnit.Framework;
using stock_trader_app_DI_csharp.StockTrader;

namespace stockTrader
{
    [TestFixture]
    public class TraderTests
    {
        private IURLReader _reader;
        private IAPIService _stockApi;
        private ITrader _trader;

        [SetUp]
        public void SetUp()
        {
            _reader = Substitute.For<IURLReader>();
            _stockApi = Substitute.For<IAPIService>();
            _trader = Substitute.For<ITrader>();
        }

        [Test] // Bid was lower than price, Buy() should return false.
        public void TestBidLowerThanPrice()
        {
            //arrange
            Trader trader = Trader.Instance();
            String symbol = "aapl";
            trader.Service = _stockApi;
            double price = 11.30;

            //act
            _stockApi.GetPrice(symbol).Returns(12.25);


            //assert
            Assert.That(trader.Buy(symbol, price), Is.EqualTo(false));

        }

        [Test] // bid was equal or higher than price, Buy() should return true.
        public void TestBidHigherThanPrice()
        {
            //arrange
            Trader trader = Trader.Instance();
            String symbol = "aapl";
            trader.Service = _stockApi;
            double price = 12.30;

            //act
            _stockApi.GetPrice(symbol).Returns(12.25);


            //assert
            Assert.That(trader.Buy(symbol, price), Is.EqualTo(true));

        }
    }
}