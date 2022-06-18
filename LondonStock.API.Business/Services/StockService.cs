using LondonStock.API.Business.Builders;
using LondonStock.API.Common.Models;
using LondonStock.API.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LondonStock.API.Business.Services
{
    public class StockService : IStockService
    {
        private readonly ICalculateAveragePriceService _calculateAveragePriceService;
        private readonly IStockRepository _stockRepository;
        private readonly IStockRequestBuilder _stockRequestBuilder;
        private readonly IStockResponseBuilder  _stockResponseBuilder;

        public StockService(
            ICalculateAveragePriceService calculateAveragePriceService,
            IStockRepository stockRepository,
            IStockRequestBuilder stockRequestBuilder,
            IStockResponseBuilder stockResponseBuilder)
        {
            _calculateAveragePriceService = calculateAveragePriceService;
            _stockRepository = stockRepository;
            _stockRequestBuilder = stockRequestBuilder;
            _stockResponseBuilder = stockResponseBuilder;
        }

        public async Task AddStockAsync(string tickerSymbol, StockRequest stockRequest)
        {
            var stock = _stockRequestBuilder.Build(tickerSymbol, stockRequest);

            await _stockRepository.AddStockAsync(stock);
        }

        public async Task<StockResponse> GetStockAsync(string tickerSymbol)
        {
            var stocks = await _stockRepository.GetStockAsync(tickerSymbol);

            if (!stocks.Any())
            {
                return null;
            }

            return new StockResponse
            {
                TickerSymbol = tickerSymbol,
                Price = _calculateAveragePriceService.Calculate(stocks)
            };
        }

        public async Task<IEnumerable<StockResponse>> GetStockAsync(List<string> tickerSymbols)
        {
            var stocks = await _stockRepository.GetStockAsync(tickerSymbols);

            return _stockResponseBuilder.Build(stocks);
        }

        public async Task<IEnumerable<StockResponse>> GetStockAsync()
        {
            var stocks = await _stockRepository.GetStockAsync();

            return _stockResponseBuilder.Build(stocks);
        }
    }
}
