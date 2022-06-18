using LondonStock.API.Business.Services;
using LondonStock.API.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace LondonStock.API.Business.Builders
{
    public class StockResponseBuilder : IStockResponseBuilder
    {
        private readonly ICalculateAveragePriceService _calculateAveragePriceService;

        public StockResponseBuilder(ICalculateAveragePriceService calculateAveragePriceService)
        {
            _calculateAveragePriceService = calculateAveragePriceService;
        }
        
        public IEnumerable<StockResponse> Build(IEnumerable<Stock> stocks)
        {
            if(!stocks.Any())
            {
                return new List<StockResponse>();
            }

            var stockResponses = new List<StockResponse>();

            var executaionGroup = stocks.GroupBy(g => g.TickerSymbol).ToList();

            foreach (var tickerGroup in executaionGroup)
            {
                var tickerStocks = stocks.Where(s => s.TickerSymbol.Equals(tickerGroup.Key)).ToList();

                stockResponses.Add(
                    new StockResponse
                    {
                        TickerSymbol = tickerGroup.FirstOrDefault().TickerSymbol,
                        Price = _calculateAveragePriceService.Calculate(tickerStocks)
                    });
            }

            return stockResponses;
        }
    }
}
