using LondonStock.API.Common.Models;
using System.Collections.Generic;

namespace LondonStock.API.Business.Services
{
    public interface ICalculateAveragePriceService
    {
        decimal Calculate(List<Stock> stocks);
    }
}
