using LondonStock.API.Business.Services;
using LondonStock.API.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LondonStock.API.Business.UnitTests.Services
{
    [TestClass]
    public class CalculateAveragePriceServiceTests
    {
        ICalculateAveragePriceService _sut;

        [TestInitialize]
        public void TestInit()
        {
            _sut = new CalculateAveragePriceService();
        }

        [TestMethod]
        public void Calculate_WhenStockIsEmpty_ThenTReturnZero()
        {
            // Arrange
            var stocks = new List<Stock>();

            // Act
            var result = _sut.Calculate(stocks);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Calculate_WhenStockIsNotEmpty_ThenTReturnAveragePrice()
        {
            // Arrange
            var stocks = GetStocks("Test1");

            var average = stocks.Select(s => s.Price).Sum() / stocks.Select(s => s.TotalShare).Sum();

            // Act
            var result = _sut.Calculate(stocks);

            // Assert
            Assert.AreEqual(average, result);
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
