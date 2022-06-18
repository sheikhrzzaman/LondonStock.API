using LondonStock.API.Business.Builders;
using LondonStock.API.Business.Services;
using LondonStock.API.Common.Models;
using LondonStock.API.Data.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LondonStock.API.Business.UnitTests.Services
{
    [TestClass]
    public class StockServiceTests
    {
        private IStockService _sut;

        private Mock<ICalculateAveragePriceService> _mockCalculateAveragePriceService;
        private Mock<IStockRepository> _mockStockRepository;
        private Mock<IStockRequestBuilder> _mockStockRequestBuilder;
        private Mock<IStockResponseBuilder> _mockStockResponseBuilder;

        private List<Stock> _stocks;

        [TestInitialize]
        public void TestInit()
        {
            _stocks = GetStocks("Test1");

            _mockCalculateAveragePriceService = new Mock<ICalculateAveragePriceService>();
            _mockStockRepository = new Mock<IStockRepository>();
            _mockStockRequestBuilder = new Mock<IStockRequestBuilder>();
            _mockStockResponseBuilder = new Mock<IStockResponseBuilder>();

            _mockCalculateAveragePriceService
                .Setup(x => x.Calculate(It.IsAny<List<Stock>>()))
                .Returns(10);

            _sut = new StockService(
                    _mockCalculateAveragePriceService.Object,
                    _mockStockRepository.Object,
                    _mockStockRequestBuilder.Object,
                    _mockStockResponseBuilder.Object);

        }

        [TestMethod]
        public async Task AddStockAsync_ThenStockAdded()
        {
            // Arrange
            var tickerSymbol = "Test1";
            var stockRequest = new StockRequest();
            var stock = new Stock();

            _mockStockRequestBuilder
                .Setup(x => x.Build(It.IsAny<string>(), It.IsAny<StockRequest>()))
                .Returns(stock);

            // Act
            await _sut.AddStockAsync(tickerSymbol, stockRequest);

            // Assert
            _mockStockRequestBuilder.Verify(x => x.Build(tickerSymbol, stockRequest), Times.Once);
            _mockStockRepository.Verify(x => x.AddStockAsync(stock), Times.Once);
        }

        [TestMethod]
        public async Task GetStockAsync_WhenStockNotFound_ThenReturnNull()
        {
            // Arrange
            var tickerSymbol = "Test1";
            var stocks = new List<Stock>();

            _mockStockRepository
                .Setup(x => x.GetStockAsync(tickerSymbol))
                .ReturnsAsync(stocks);

            // Act
            var result = await _sut.GetStockAsync(tickerSymbol);

            // Assert
            Assert.IsNull(result);
            _mockCalculateAveragePriceService.Verify(x => x.Calculate(It.IsAny<List<Stock>>()), Times.Never);
        }

        [TestMethod]
        public async Task GetStockAsync_WhenStocksFound_ThenReturnStockResponse()
        {
            // Arrange
            var tickerSymbol = "Test1";
            var stocks = GetStocks(tickerSymbol);

            _mockStockRepository
                .Setup(x => x.GetStockAsync(tickerSymbol))
                .ReturnsAsync(stocks);

            // Act
            var result = await _sut.GetStockAsync(tickerSymbol);

            // Assert
            Assert.AreEqual(tickerSymbol, result.TickerSymbol);
            _mockCalculateAveragePriceService.Verify(x => x.Calculate(stocks), Times.Once);
        }

        [TestMethod]
        public async Task GetStockAsync_WhenTickerSymbolsFound_ThenReturnStockResponse()
        {
            // Arrange
            var tickerSymbol1 = "Test1";
            var stocks = GetStocks(tickerSymbol1);

            var tickerSymbol2 = "Test2";
            stocks.AddRange(GetStocks(tickerSymbol2));

            var tickerSymbols = new List<string> { tickerSymbol1, tickerSymbol2 };

            _mockStockRepository
                .Setup(x => x.GetStockAsync(tickerSymbols))
                .ReturnsAsync(stocks);

            // Act
            var result = await _sut.GetStockAsync(tickerSymbols);

            // Assert
            _mockStockRepository
               .Verify(x => x.GetStockAsync(tickerSymbols), Times.Once);

            _mockStockResponseBuilder.Verify(x => x.Build(stocks), Times.Once);
        }

        [TestMethod]
        public async Task GetStockAsync_WhenTickerSymbolsFound_ThenReturnAllStocksResponse()
        {
            // Arrange
            var tickerSymbol1 = "Test1";
            var stocks = GetStocks(tickerSymbol1);

            var tickerSymbol2 = "Test2";
            stocks.AddRange(GetStocks(tickerSymbol2));

            _mockStockRepository
                .Setup(x => x.GetStockAsync())
                .ReturnsAsync(stocks);

            // Act
            var result = await _sut.GetStockAsync();

            // Assert
            _mockStockRepository
               .Verify(x => x.GetStockAsync(), Times.Once);

            _mockStockResponseBuilder.Verify(x => x.Build(stocks), Times.Once);
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
