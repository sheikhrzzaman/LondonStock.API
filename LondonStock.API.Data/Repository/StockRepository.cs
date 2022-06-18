using LondonStock.API.Common.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace LondonStock.API.Data.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly string _connectionString;

        public StockRepository(IConfiguration configuration)
        {
            _connectionString = configuration["StockSqlSeConnectionStrig"];

            if(string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new ConfigurationErrorsException("Stock db connection string missinh");
            }

        }
        public async Task<List<Stock>> GetStockAsync(string tickerSymbol)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var sql = GetSqlString() + $" WHERE TickerSymbol = {tickerSymbol}";

            var stocks = await connection.QueryAsync<Stock>(sql);

            return stocks.ToList();
        }

        public async Task<List<Stock>> GetStockAsync(List<string> tickerSymbols)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var whereClause = " WHERE TickerSymbol IN (" + string.Join(",", tickerSymbols.Select(item => $"'{item}'")) + ")";

            var sql = GetSqlString() + whereClause;

            var stocks = await connection.QueryAsync<Stock>(sql);

            return stocks.ToList();
        }

        public async Task<List<Stock>> GetStockAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var sql = GetSqlString();

            var stocks = await connection.QueryAsync<Stock>(sql);

            return stocks.ToList();
        }

        public async Task AddStockAsync(Stock stock)
        {
            var sqlInsert = $@" INSERT INTO Stock.StockTransactions
                                    (TransactionId,
                                    TickerSymbol,
                                    Price,
                                    TotalShare,
                                    BrokerId,
                                    TransactionDate)
                            VALUES ({stock.TransactionId},
                                    {stock.TickerSymbol},
                                    {stock.Price},
                                    {stock.TotalShare},
                                    {stock.TransactionDate},)";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            await connection.ExecuteAsync(sqlInsert);
        }

        private string GetSqlString()
        {
            return $@" SELECT TransactionId,
                              TickerSymbol,
                              Price,
                              TotalShare,
                              BrokerId,
                              TransactionDate
                        FROM Stock.StockTransactions";
        }
    }
}
