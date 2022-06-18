using LondonStock.API.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace LondonStock.API.Business.Services
{
    public class CalculateAveragePriceService : ICalculateAveragePriceService
    {
        public decimal Calculate(List<Stock> stocks)
        {
            var totalShare = stocks.Select(s => s.TotalShare).Sum();

            if (totalShare <= 0)
            {
                return 0;
            }

            return stocks.Select(s => s.Price).Sum()/totalShare;
        }
    }
}
