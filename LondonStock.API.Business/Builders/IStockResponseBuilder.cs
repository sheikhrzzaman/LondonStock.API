using LondonStock.API.Common.Models;
using System.Collections.Generic;

namespace LondonStock.API.Business.Builders
{
    public interface IStockResponseBuilder
    {
        IEnumerable<StockResponse> Build(IEnumerable<Stock> stocks);
    }
}
