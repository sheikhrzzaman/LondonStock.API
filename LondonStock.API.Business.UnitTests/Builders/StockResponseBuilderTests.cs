using LondonStock.API.Business.Builders;
using LondonStock.API.Business.Services;
using LondonStock.API.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LondonStock.API.Business.UnitTests.Builders
{
    [TestClass]
    public class StockResponseBuilderTests
    {
        private IStockResponseBuilder _sut;

        private Mock<ICalculateAveragePriceService> _mockCalculateAveragePriceService;

        private List<Stock> _stocks;
        private decimal _average;


        [TestInitialize]
        public void TestInit()
        {
            _average = 23 ;
            _mockCalculateAveragePriceService = new Mock<ICalculateAveragePriceService>();

            _mockCalculateAveragePriceService
                .Setup(x => x.Calculate(It.IsAny<List<Stock>>()))
                .Returns(_average);

            _sut = new StockResponseBuilder(_mockCalculateAveragePriceService.Object);
        }

        [TestMethod]
        public void Build_WhenStockIsEmpty_ThenReturnEmptyResponse()
        {
            // Arrange
            var stocks = new List<Stock>();

            // Act
            var result = _sut.Build(stocks);

            // Assert
            Assert.IsFalse(result.Any());

            _mockCalculateAveragePriceService
                   .Verify(x => x.Calculate(It.IsAny<List<Stock>>()), Times.Never);
        }

        [TestMethod]
        public void Build_WhenStockIsNotEmpty_ThenReturnResponse()
        {
            // Arrange
            var tickerSymbol1 = "Test1";
            var tickerStock1 = GetStocks(tickerSymbol1);

            var tickerSymbol2 = "Test2";
            var tickerStock2 = GetStocks(tickerSymbol2);

            var stocks = tickerStock1;
            stocks.AddRange(tickerStock2);

            // Act
            var result = _sut.Build(stocks);

            // Assert
            Assert.AreEqual(2, result.Count());
            var response1 = result.FirstOrDefault(x => x.TickerSymbol.Equals(tickerSymbol1, StringComparison.OrdinalIgnoreCase));
            var response2 = result.FirstOrDefault(x => x.TickerSymbol.Equals(tickerSymbol1, StringComparison.OrdinalIgnoreCase));

            Assert.AreEqual(_average, response1.Price);
            Assert.AreEqual(_average, response2.Price);

            _mockCalculateAveragePriceService
                   .Verify(x => x.Calculate(It.IsAny<List<Stock>>()), Times.Exactly(2));
        }

        private List<Stock> GetStocks(string tickerSymbol)
        {
            return new List<Stock>
            {
                new Stock
                {
                    TransactionId = Guid.NewGuid(),
                    BrockerId = "B123",
                    TickerSymbol = tickerSymbol,
                    Price = 20,
                    TotalShare = 10,
                    TransactionDate = DateTime.UtcNow
                },
                new Stock
                {
                    TransactionId = Guid.NewGuid(),
                    BrockerId = "B123",
                    TickerSymbol = tickerSymbol,
                    Price = 20,
                    TotalShare = 10,
                    TransactionDate = DateTime.UtcNow
                }

            };
        }

    }
}
