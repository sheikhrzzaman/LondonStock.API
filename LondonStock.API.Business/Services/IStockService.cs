using LondonStock.API.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStock.API.Business.Services
{
    public interface IStockService
    {
        Task AddStockAsync(string tickerSymbol, StockRequest stockRequest);

        Task<StockResponse> GetStockAsync(string tickerSymbol);

        Task<IEnumerable<StockResponse>> GetStockAsync(List<string> tickerSymbols);

        Task<IEnumerable<StockResponse>> GetStockAsync();
    }
}
