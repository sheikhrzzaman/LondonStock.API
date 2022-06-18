using LondonStock.API.Business.Builders;
using LondonStock.API.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LondonStock.API.Business.UnitTests.Builders
{
    [TestClass]
    public class StockRequestBuilderTests
    {
        private IStockRequestBuilder _sut;

        [TestMethod]
        public void Build_ThenAlwaysReturnStock()
        {
            // Arrange
            var sut = new StockRequestBuilder();

            var stockRequest = new StockRequest { BrockerId = "B123", TotalShare = 29, Price = 100 };
            var tickerSymbol = "Test230";

            // Act
            var result = sut.Build(tickerSymbol, stockRequest);

            // Assert
            Assert.AreEqual(tickerSymbol, result.TickerSymbol);
            Assert.AreEqual(stockRequest.BrockerId, result.BrockerId);
            Assert.AreEqual(stockRequest.Price, result.Price);
            Assert.AreEqual(stockRequest.TotalShare, result.TotalShare);
        }
    }
}
