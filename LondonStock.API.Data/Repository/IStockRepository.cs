using LondonStock.API.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStock.API.Data.Repository
{
    public interface IStockRepository
    {
        Task AddStockAsync(Stock stock);

        Task<List<Stock>> GetStockAsync(string tickerSymbol);

        Task<List<Stock>> GetStockAsync(List<string> tickerSymbols);

        Task<List<Stock>> GetStockAsync();
    }
}
