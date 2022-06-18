using FluentValidation;
using LondonStock.API.Business.Builders;
using LondonStock.API.Business.Services;
using LondonStock.API.Business.Validations;
using LondonStock.API.Common.Models;
using LondonStock.API.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace LondonStock.API
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddSingleton<IStockService, StockService>();
            services.AddSingleton<IStockRepository, StockRepository>();
            services.AddSingleton<ICalculateAveragePriceService, CalculateAveragePriceService>();
            services.AddSingleton<IStockRequestBuilder, StockRequestBuilder>();
            services.AddSingleton<IStockResponseBuilder, StockResponseBuilder>();
            services.AddSingleton<IValidator<StockRequest>, StockRequestValidator>();
        }
    }
}
