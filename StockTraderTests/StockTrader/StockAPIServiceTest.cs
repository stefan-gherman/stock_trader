using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using stock_trader_app_DI_csharp.StockTrader;
using System;
using System.Net;


namespace stockTrader
{
    [TestFixture]
    public class StockAPIServiceTest
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
        [Test] // everything works
        public void TestGetPriceNormalValues()
        {
            //arrange
            string symbol = "aapl";
            _stockApi.GetPrice(symbol).Returns(23.21);

            //act
            double price = _stockApi.GetPrice(symbol);
            _trader.Buy(symbol, price);

            //assert
            _trader.Received().Buy(Arg.Any<string>(), Arg.Any<double>());
        }

        [Test] // readFromURL threw an exception
        public void TestGetPriceServerDown()
        {
            ////arrange
            //_reader.ReadFromUrl("http://www.test.com/{0}").Returns(x => { throw new Exception(); });
            //var aPIService = StockAPIService.Instance();
            //string symbol = "aapl";
            RemoteURLReader reader = RemoteURLReader.Instance();
            string url = @"http://www.test.com"; 
            ////act 
            //aPIService.Reader = _reader;
            //aPIService.APIPath = "http://www.test.com/{0}";

            ////assert
            //Assert.Throws<Exception>(()=> aPIService.GetPrice(symbol));
            Assert.Throws<WebException>(() => reader.ReadFromUrl(url));
        }

        [Test] // readFromURL returned wrong JSON
        public void TestGetPriceMalformedResponse()
        {
            //arrange
            var stockApi = StockAPIService.Instance();
            var dummy = "dummy";
            //act

            _reader.ReadFromUrl(dummy).Throws(new Newtonsoft.Json.JsonReaderException());
            stockApi.Reader = _reader;
            stockApi.APIPath = dummy;

            //assert
            Assert.Throws<Newtonsoft.Json.JsonReaderException>(() => stockApi.GetPrice("aapl"));
        }
    }
}