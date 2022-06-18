using LondonStock.API.Common.Models;

namespace LondonStock.API.Business.Builders
{
    public interface IStockRequestBuilder
    {
        Stock Build(string tickerSymbol, StockRequest stockRequest);
    }
}
