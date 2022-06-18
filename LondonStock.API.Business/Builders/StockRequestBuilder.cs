using LondonStock.API.Common.Models;

namespace LondonStock.API.Business.Builders
{
    public class StockRequestBuilder : IStockRequestBuilder
    {
        public Stock Build(string tickerSymbol, StockRequest stockRequest)
        {
            return new Stock
            {
                TickerSymbol = tickerSymbol,
                BrockerId = stockRequest.BrockerId,
                TotalShare = stockRequest.TotalShare,
                Price = stockRequest.Price
            };
        }
    }
}
